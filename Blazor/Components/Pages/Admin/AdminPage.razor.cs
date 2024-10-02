using Microsoft.AspNetCore.Components.Forms;
using Microsoft.JSInterop;
using Models;

namespace Blazor.Components.Pages.Admin
{
    public partial class AdminPage
    {
        private long maxFileSize = 1024 * 1024 * 50; // 50 MB max fil størrelse, det sidste nummer er til at ændre MB
        private List<string> errors = new();
        private Users newUser = new();
        private IBrowserFile? file;
        private List<Users>? users;


        private async Task LoadUsers() //api kald
        {
            users = await sql.LoadData<Users>(
                "dbo.spUsers_GetAll",
                "DbConnection",
                null);
        }
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
            await LoadUsers();
        }

        private async Task SubmitForm() //api kald
        {
            errors.Clear();

            try
            {
                string relativePath = await CaptureFile();
                newUser.FileName = relativePath;

                await sql.SaveData("dbo.spUsers_Insert", "DbConnection", newUser);

                newUser = new();
                file = null;
                errors.Clear();

                await LoadUsers();
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
