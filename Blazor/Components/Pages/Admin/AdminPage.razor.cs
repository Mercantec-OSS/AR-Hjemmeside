using Microsoft.AspNetCore.Components.Forms;
using Microsoft.JSInterop;
using Models;
using Newtonsoft.Json;

namespace Blazor.Components.Pages.Admin
{
    public partial class AdminPage
    {
        //private long maxFileSize = 1024 * 1024 * 50; // 50 MB max fil størrelse, det sidste nummer er til at ændre MB
        //private List<string> errors = new(); bliver ikke brugt
        //private Users newUser = new(); bliver ikke brugt i forbindelse med metoden SubmitForm()
        //private IBrowserFile? file; bliver ikke brugt i forbindelse med metoden SubmitForm()
        private List<Users>? users;


        private async Task LoadUsers()
        {
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync("https://localhost:7013/api/user");

                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();

                    users = JsonConvert.DeserializeObject<List<Users>>(content);
                }
            }
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



        private async Task DeleteUser(int id)
        {
            using (var client = new HttpClient())
            {
                HttpResponseMessage response = await client.DeleteAsync("");

                if (response.IsSuccessStatusCode)
                {
                    //kan fx åbne en Dialog eller Toastr
                }
            }
        }

        //private async Task SubmitForm() //er ikke forbundet til noget
        //{
        //    errors.Clear();

        //    try
        //    {
        //        string relativePath = await CaptureFile();
        //        newUser.FileName = relativePath;

        //        await sql.SaveData("dbo.spUsers_Insert", "DbConnection", newUser);

        //        newUser = new();
        //        file = null;
        //        errors.Clear();

        //        await LoadUsers();
        //    }
        //    catch (Exception ex)
        //    {
        //        errors.Add($"Error: {ex.Message}");
        //    }
        //}

        //private void LoadFiles(InputFileChangeEventArgs e) bliver ikke brugt i forbindelse med SubmitForm()
        //{
        //    file = e.File;
        //}

        //private async Task<string> CaptureFile() bliver ikke brugt i forbindelse med SubmitForm()
        //{
        //    if (file is null)
        //    {
        //        return "";
        //    }

        //    try
        //    {
        //        string newFileName = Path.ChangeExtension(
        //            Path.GetRandomFileName(),
        //            Path.GetExtension(file.Name));

        //        string path = Path.Combine(config.GetValue<string>("FileStorage")!, "Test", newFileName);

        //        string relativePath = Path.Combine("Test", newFileName);

        //        Directory.CreateDirectory(Path.Combine(config.GetValue<string>("FileStorage")!, "Test"));

        //        await using FileStream fs = new(path, FileMode.Create);
        //        await file.OpenReadStream(maxFileSize).CopyToAsync(fs);

        //        return relativePath;
        //    }
        //    catch (Exception ex)
        //    {
        //        errors.Add($"Error with file {file.Name} Error: {ex.Message}");
        //        throw;
        //    }

        //}
    }
}
