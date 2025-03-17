using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace Project2.Models
{
    public class ImageUploader
    {
        private static string UploadFolder = "~/ProductImages/"; // Folder penyimpanan gambar

        public static string UploadFile(HttpPostedFile file, string productId)
        {
            if (file == null || file.ContentLength == 0)
                return null;

            // Dapatkan ekstensi file
            string extension = Path.GetExtension(file.FileName).ToLower();
            string[] allowedExtensions = { ".jpg", ".jpeg", ".png", ".gif" };

            // Cek apakah ekstensi diperbolehkan
            if (Array.IndexOf(allowedExtensions, extension) < 0)
                throw new Exception("Format gambar tidak didukung. Gunakan JPG, PNG, atau GIF.");

            // Nama file baru berdasarkan ID produk
            string newFileName = productId + extension;
            string savePath = HttpContext.Current.Server.MapPath(UploadFolder + newFileName);

            // Simpan file ke server
            file.SaveAs(savePath);

            // Kembalikan path yang disimpan di database
            return "ProductImages/" + newFileName;
        }
    }
}