using Microsoft.AspNetCore.Components.Forms;
using Microsoft.JSInterop;
using Models;
using Newtonsoft.Json;
using System.Text;

namespace Blazor.Components.Pages
{
    public partial class CreateUser
    {
        private long maxFileSize = 1024 * 1024 * 50; // 50 MB max fil størrelse, det sidste nummer er til at ændre MB
        private List<string> errors = new();
        private Users newUser = new();
        private IBrowserFile? file;
        private List<Users>? users;


        //private async Task LoadUsers()
        //{
        //    users = await sql.LoadData<Users>(
        //        "dbo.spUsers_GetAll",
        //        "DbConnection",
        //        null);
        //}
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await JSRuntime.InvokeVoidAsync("import", "https://ajax.googleapis.com/ajax/libs/model-viewer/3.5.0/model-viewer.min.js");
            }
        }
        private string CreateWebPath(string relativePath)
        {
            return Path.Combine(config.GetValue<string>("WebStorageRoot")!, relativePath);
        }

        protected override async Task OnInitializedAsync()
        {
            //await LoadUsers();
        }

        private async Task SubmitForm()
        {
            errors.Clear();

            try
            {
                //string relativePath = await CaptureFile();
                //newUser.FileName = relativePath;

                using (HttpClient client = new HttpClient())
                {
                    StringContent content = new StringContent(JsonConvert.SerializeObject(newUser), Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await client.PostAsync("https://localhost:7013/api/user/CreateUser", content);

                }

                newUser = new();
                file = null;
                //errors.Clear();

                //await LoadUsers();
            }
            catch (Exception ex)
            {
                errors.Add($"Error: {ex.Message}");
            }
        }

        private void LoadFiles(InputFileChangeEventArgs e)
        {
            file = e.File;
        }

        private async Task<string> CaptureFile()
        {
            if (file is null)
            {
                return "";
            }

            try
            {
                string newFileName = Path.ChangeExtension(
                    Path.GetRandomFileName(),
                    Path.GetExtension(file.Name));

                string path = Path.Combine(config.GetValue<string>("FileStorage")!, "Test", newFileName);

                string relativePath = Path.Combine("Test", newFileName);

                Directory.CreateDirectory(Path.Combine(config.GetValue<string>("FileStorage")!, "Test"));

                await using FileStream fs = new(path, FileMode.Create);
                await file.OpenReadStream(maxFileSize).CopyToAsync(fs);

                return relativePath;
            }
            catch (Exception ex)
            {
                errors.Add($"Error with file {file.Name} Error: {ex.Message}");
                throw;
            }

        }

    }
}
