using System.Security.Cryptography;
using System.Text;
using SixLabors.ImageSharp.Formats.Jpeg;

namespace Willowstore.Service
{
    public class WebFile
    {
        const string FolderPrefix = "./wwwroot";

        public string GetWebFileName(string filename)
        {
            string dir = GetWebFileFolder(filename);
            CreateFolder(FolderPrefix + dir);

            return dir + "/" + Path.GetFileNameWithoutExtension(filename) + ".jpeg";
        }

        public string GetWebFileFolder(string filename)
        {
            MD5 md5hash = MD5.Create();
            byte[] inputBytes = Encoding.ASCII.GetBytes(filename);
            byte[] hashBytes = md5hash.ComputeHash(inputBytes);

            string hash = Convert.ToHexString(hashBytes);

            // folder predix
            return "/images/" + hash.Substring(0, 2) + "/" + hash.Substring(0, 4);
        }

        public void CreateFolder(string dir)
        {
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);
        }

        public async Task UploadAndResizeImage(Stream fileStream, string fileName, int newWidth, int newHeight)
        {
            using (Image image = await Image.LoadAsync(fileStream))
            {
                int aspectWidth = newWidth;
                int aspectHeight = newHeight;

                if (image.Width / (image.Height / (float)newHeight) > newWidth)
                    aspectHeight = (int)(image.Height / (image.Width / (float)newHeight));
                else
                    aspectWidth = (int)(image.Width / (image.Height / (float)newHeight));

                image.Mutate(x => x.Resize(aspectWidth, aspectHeight, KnownResamplers.Lanczos3));

                await image.SaveAsJpegAsync(FolderPrefix + fileName, new JpegEncoder() { Quality = 75 });
            }
        }
    }
}
