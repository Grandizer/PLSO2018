
vm = new Vue({
	el: "#app",
	data: {
		Records: JSON.parse(document.getElementById("hdnRecords").value),
		Selected: [],
	},
	computed: {
		noneSelected() {
			return this.Selected.length === 0;
		},
	},
	methods: {
		swapSelections(check) {
			this.Selected = [];

			if (check === true) {
				for (let r of this.Records)
					this.Selected.push(r.ID);
			}
		},
		approveSelected() {
			let model = {
				IDs: this.Selected
			};

			console.log("Saving:", model);

			fetch('/api/records/approve', {
				headers: {
					//'XSRF-TOKEN': document.getElementsByName('__RequestVerificationToken')[0].value,
					'content-type': 'application/json; charset=utf-8',
					//'X-Content-Type-Options': 'nosniff'
				},
				credentials: 'include',
				body: JSON.stringify(model),
				method: 'POST'
			}).then((response) => {
				return response;
			}).then((response) => {
				return response.json();
			}).then(function (data) {
				console.log("Done:", data);
			}).catch(function (error) {
				console.log("Error", error);
			});
		}
	}
});
