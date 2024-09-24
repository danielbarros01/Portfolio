using Portfolio.Entities.Interfaces;
using Portfolio.Services.Storage;

namespace Portfolio.Helpers
{
    public class FilesUtil
    {
        //public async static Task SaveReadme<TEntityCreation, TEntity>
        //    (TEntityCreation entityCreation, TEntity entity, IFileStorage fileStorage, String container)
        //    where TEntityCreation : class, IReadme
        //{
        //    var readme = entityCreation.Readme;
        //    if (readme != null)
        //    {
        //        entity = await SaveFile(readme, fileStorage, container);
        //    }
        //}

        public static async Task<String> GetUrlFile<TFile>(
            TFile file, IFileStorage fileStorage, String container,
            ActionForFileType actionType, String currentUrl = null
            ) where TFile : IFormFile
        {
            var url = "";

            using (var memoryStream = new MemoryStream())
            {
                await file.CopyToAsync(memoryStream);
                var content = memoryStream.ToArray();
                var extension = Path.GetExtension(file.FileName);

                if (actionType == ActionForFileType.Save)
                {
                    url = await fileStorage.SaveFile(
                        content,
                        extension,
                        container,
                        file.ContentType
                     );
                }
                else if (actionType == ActionForFileType.Edit)
                {
                    url = await fileStorage.EditFile(
                        content,
                        extension,
                        container,
                        currentUrl,
                        file.ContentType
                     );
                }
                else
                {
                    await fileStorage.DeleteFile(currentUrl, container);
                }

            }

            return url;
        }
    }
}
