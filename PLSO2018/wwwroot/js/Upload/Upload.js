
vm = new Vue({
	el: "#app",
	data: {
		images: [],
	},
	computed: {

	},
	methods: {
		viewImage() {

		},
		handleDragOver(evt) {
			evt.stopPropagation();
			evt.preventDefault();
			evt.dataTransfer.dropEffect = 'copy';
			document.getElementById('drop-zone').classList.add("over-drop-zone");
		},
		handleDragOut(evt) {
			document.getElementById('drop-zone').classList.remove("over-drop-zone");
		},
		handleFileDrop(event) {
			let self = this;
			let files = event.dataTransfer.files;
			event.stopPropagation();
			event.preventDefault();

			document.getElementById('drop-zone').classList.remove("over-drop-zone");

			for (let i = 0; i < files.length; i++) {
				let file = files[i];
				self.addFileToList(file);
			}
		},
		stopRandomDrops(evt) {
			evt.preventDefault();
			evt.stopPropagation();
		},
		addFileToList(file, wasPasted) {
			// Only process image files
			console.log('Adding file', file.type);
			if (!file.type.match('image.*'))
				return;

			console.log('  Is an image');
			let self = this;
			let fReader = new FileReader();

			fReader.onload = function (e) {
				let image = {
					FileDataBase64: e.target.result, // data:${FileType};base64,...
					Name: (wasPasted ? ('Pasted_' + new Date().getTime()) : file.name),
					Blob: file
				};

				self.images.push(image);
				self.saveImage(image);
			};

			fReader.readAsDataURL(file);
		},
		determineFileFormatId(file) {
			let Result = 0;

			switch (file.type) {
				case "image/gif":
					Result = 1;
					break;
				case "image/png":
					Result = 2;
					break;
				case "image/jpeg":
					Result = 3;
					break;
				default:
					toastr.warning('Unhandled image file type "' + file.type + '"');
					break;
			}

			return Result;
		},
		filePrefix(file) {
			let FileType = "";

			switch (this.getFileExtension(file.Name)) {
				case 1:
					FileType = "image/gif";
					break;
				case 2:
					FileType = "image/png";
					break;
				case 3:
					FileType = "image/jpeg";
					break;
			}

			return file.Id > 0 ? `data:${FileType};base64,` : '';
		},
		saveImage(image) {
			var formData = new FormData();
			let self = this;

			formData.append('file', image.Blob);
			formData.append('name', image.Name);

			console.log("  Saving Image", formData);

			this.fetchSimple(
				'/api/images/upload',
				'POST',
				formData,
				function (data) {
					console.log('SAVED');
					// Need to remove the pasting prefix of: data:image/png;base64,
					// This parallels what the FileDataBase64 is when coming back from server
					let prefix = self.filePrefix(image);
					image.FileDataBase64 = image.FileDataBase64.substring(prefix.length);
					//alert("Saved image");
					//toastr.success('Successfully saved the image', 'Image Upload');
					return;
				},
				function (error) {
					console.error('Error: ', error);
				},
				function () {

				}
			);
		},
		randomNumber(minimum, maximum) {
			return Math.floor(Math.random() * (maximum - minimum + 1)) + minimum;
		},
		checkStatus(response) {
			if (response.status >= 200 && response.status < 300) {
				// 204 == Good endpoint but no data
				return response;
			} else if (response.status === 409) {
				let error = new Error('Outdated data');
				error.status = 409;
				error.response = response;
				console.warn('There is a 409 Conflict - throwing and error', response);
				throw error;
			} else {
				let error = new Error(response.statusText);
				error.response = response;
				throw error;
			}
		},
		parseJSON(response) {
			// If getting 'unexpected end of json data' 
			// Chances are the C# code is doing the following:
			//   return Ok();
			// Change it to the following:
			//   return Ok(true);
			// Unless there is something more logical to return like an id.
			try {
				// Perhaps we do this instead: https://stackoverflow.com/a/48266945/373438
				return (response.status === 204) ? {} : response.json();
			} catch (e) {
				return {};
			}
		},
		fetchWithToken(fullUrl, method, data, referrer, successMethod, errorMethod, doneMethod) {
			fetch(fullUrl, {
				credentials: 'include',
				headers: {
					'XSRF-TOKEN': document.getElementsByName('__RequestVerificationToken')[0].value,
					'content-type': 'application/json; charset=utf-8',
					'X-Content-Type-Options': 'nosniff'
				},
				referrer: referrer,
				referrerPolicy: 'no-referrer-when-downgrade',
				body: data,
				method: method,
				mode: 'cors'
			}).then((response) => {
				return this.checkStatus(response);
			}).then((response) => {
				return this.parseJSON(response);
			}).then((data) => {
				if (data === 409) {
					if (errorMethod !== undefined)
						return errorMethod(409);
					else
						return 409;
				}
				if (successMethod !== undefined) {
					successMethod(data);
					if (doneMethod)
						doneMethod();
				} else {
					if (doneMethod)
						doneMethod();
				}
			}).catch((error) => {
				if (errorMethod !== undefined) {
					errorMethod(error);
					if (doneMethod)
						doneMethod();
				} else {
					if (doneMethod)
						doneMethod();
				}
			});
		},
		fetchSimple(fullUrl, method, data, successMethod, errorMethod, doneMethod) {
			fetch(fullUrl, {
				credentials: 'include',
				body: data,
				method: method
			}).then((response) => {
				return this.checkStatus(response);
			}).then((response) => {
				return this.parseJSON(response);
			}).then((data) => {
				if (successMethod !== undefined) {
					successMethod(data);
					if (doneMethod)
						doneMethod();
				} else {
					if (doneMethod)
						doneMethod();
				}
			}).catch((error) => {
				if (errorMethod !== undefined) {
					errorMethod(error);
					if (doneMethod)
						doneMethod();
				} else {
					if (doneMethod)
						doneMethod();
				}
			});
		},
		getFileExtension(filename) {
			return (/[.]/.exec(filename)) ? /[^.]+$/.exec(filename) : undefined;
		},
	},
	mounted() {
		let self = this;

		document.addEventListener('paste', async e => {
			let items;

			if (navigator.clipboard) {
				console.log('nc', navigator.clipboard);
				navigator.clipboard.read().then(function (data) {
					for (var i = 0; i < data.items.length; i++) {
						if (data.items[i].type == "text/plain") {
							console.log('Your string: ', data.items[i].getAs('text / plain'));
						} else {
							console.error('No text / plain data on clipboard.');
						}
					}
				});
			} else {
				e.clipboardData.items;
			}

			for (var i = 0; i < items.length; i++) {
				console.log('Processing Index', i);
				self.addFileToList(items[i].getAsFile(), true);
			}
		});
		//document.onpaste = function (event) {
		//	let items = (event.clipboardData || event.originalEvent.clipboardData).items;
		//	console.log('Navigator', navigator);
		//	console.log('Pasted Items Found', items.length);
		//	for (var i = 0; i < items.length; i++) {
		//		console.log('Processing Index', i);
		//		self.addFileToList(items[i].getAsFile(), true);
		//	}
		//};

		let dropZone = document.getElementById('drop-zone');

		dropZone.addEventListener('dragover', self.handleDragOver, false);
		dropZone.addEventListener('dragleave', self.handleDragOut, false);
		dropZone.addEventListener('drop', self.handleFileDrop, false);

		//document.addEventListener('drop', self.stopRandomDrops, false);
		//document.addEventListener('dragover', self.stopRandomDrops, false);
	}
});
