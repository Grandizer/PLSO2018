using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace PLSO2018.Controllers.API {

	[Route("api/[controller]")]
	[ApiController]
	public class ImagesController : ControllerBase {

		private readonly IHostingEnvironment environment;

		public ImagesController(IHostingEnvironment environment) {
			this.environment = environment;
		}

		[Route("upload")]
		[HttpPost]
		public async Task<IActionResult> UploadImageAsync([FromForm]IFormFile model) {
			var form = await Request.ReadFormAsync();
			var file = form.Files.First();
			var data = new byte[file.Length];

			var Folder = new DirectoryInfo(Path.Combine(environment.WebRootPath, "Uploads\\WaitingApproval"));
			Folder.Create();

			using (var stream = file.OpenReadStream()) {
				var fileBytes = await stream.ReadAsync(data);

				using (FileStream fs = new FileStream(Path.Combine(Folder.FullName, file.FileName), FileMode.Create, FileAccess.Write)) {
					fs.Write(data, 0, fileBytes);
					fs.Close();
				}
			}

			//var result = await _fileUploadService.CreateFileUploadAsync(model, data);

			return Ok(true);
	}

	public async Task<string> UploadFileAsBlob(Stream stream, string filename) {
		CloudStorageAccount storageAccount = CloudStorageAccount.Parse("ConnectionString:StorageConnectionString"); // config setting
		CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
		CloudBlobContainer container = blobClient.GetContainerReference("profileimages"); // Retrieve a reference to a container.
		CloudBlockBlob blockBlob = container.GetBlockBlobReference(filename);

		await blockBlob.UploadFromStreamAsync(stream);

		stream.Dispose();

		return blockBlob?.Uri.ToString();
	}

}
}
