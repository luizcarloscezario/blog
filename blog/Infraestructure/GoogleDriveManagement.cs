using System;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Drive.v3;
using Google.Apis.Drive.v3.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace blog.Infraestructure
{
    public class GoogleDriveManagement : IDisposable, IGoogleDriveManagement
    {
        protected DriveService driveService {get; set;}


        bool disposed = false;
        static string[] Scopes = { DriveService.Scope.DriveReadonly };
        static string ApplicationName = "Blog";



        public GoogleDriveManagement()
        {
          Dispose(false);
          Login();
        }



        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);           
        }


        protected virtual void Dispose(bool disposing)
        {
            if(disposed)
            return;

            disposed = true;
        }



        public async Task<string> UploadAsync(FileStream file)
        {
            
            var fileMetaData = new Google.Apis.Drive.v3.Data.File() {Name = "photo.jpg" };
            
            FilesResource.CreateMediaUpload request;

            request =  driveService.Files.Create(fileMetaData, file, "image/jpge");

            request.Fields = "id";
            request.Upload();

            var response =request.ResponseBody;

            Console.WriteLine("File ID: " + response.Id);

            return  response.Id;
        }    


        public async Task<bool> DeleteAsync(string file)
        {
            return true;
        }


        public async Task<bool> IsExist(string file)
        {
            return false;
        }

        private void Login()
        {
            Google.Apis.Auth.OAuth2.UserCredential credenciais;
 
             using (var stream = new System.IO.FileStream("credentials.json", System.IO.FileMode.Open, System.IO.FileAccess.Read))
             {
                 var diretorioAtual = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
                 var diretorioCredenciais = System.IO.Path.Combine(diretorioAtual, "credential");

                 credenciais = Google.Apis.Auth.OAuth2.GoogleWebAuthorizationBroker.AuthorizeAsync(
                     Google.Apis.Auth.OAuth2.GoogleClientSecrets.Load(stream).Secrets,
                     new[] { Google.Apis.Drive.v3.DriveService.Scope.DriveReadonly },
                     "user",
                     System.Threading.CancellationToken.None,
                     new Google.Apis.Util.Store.FileDataStore(diretorioCredenciais, true)).Result;
             }

        }

        public void Start()
        {
            UserCredential credential;

            using (var stream =
                new FileStream("credentials.json", FileMode.Open, FileAccess.Read))
            {
                // The file token.json stores the user's access and refresh tokens, and is created
                // automatically when the authorization flow completes for the first time.
                string credPath = "token.json";
                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    Scopes,
                    "user",
                    CancellationToken.None,
                    new FileDataStore(credPath, true)).Result;
                Console.WriteLine("Credential file saved to: " + credPath);
            }

            // Create Drive API service.
            var service = new DriveService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });

            // Define parameters of request.
            FilesResource.ListRequest listRequest = service.Files.List();
            listRequest.PageSize = 10;
            listRequest.Fields = "nextPageToken, files(id, name)";

            // List files.
            IList<Google.Apis.Drive.v3.Data.File> files = listRequest.Execute()
                .Files;
            Console.WriteLine("Files:");
            if (files != null && files.Count > 0)
            {
                foreach (var file in files)
                {
                    Console.WriteLine("{0} ({1})", file.Name, file.Id);
                }
            }
            else
            {
                Console.WriteLine("No files found.");
            }
            Console.Read();

        }

       

     
    }        
    
}