namespace ECommorceProje.Tools
{
    public class FileHelper
    {
        public static async Task<string> FileLoaderAsync(IFormFile? formFile, string klasorYolu = "/wwwroot/Images/")
        {
            string dosyaAdi = "";

            if (formFile is not null)
            {
                dosyaAdi = formFile.FileName;
                string dizin = Directory.GetCurrentDirectory() + klasorYolu + dosyaAdi;
                using var stream = new FileStream(dizin, FileMode.Create);
                await formFile.CopyToAsync(stream);
            }

            return dosyaAdi;
        }
        public static bool FileRemover(string fileName, string filePath = "/wwwroot/Images/")
        {
            string directory = Directory.GetCurrentDirectory() + filePath + fileName; // dosyayı sileceğimiz konum

            if (Directory.Exists(directory)) // verilen dizindeki dosyayı sil
            {
                File.Delete(directory); // işlem sonucunun başarılı olduğunu dönüyoruz
                return true;
            }
            return false; // buraya geldiyse dosya silinememeiştir.
        }
    }
}
