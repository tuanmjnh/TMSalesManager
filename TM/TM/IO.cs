using System;
//using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace TM
{
    public class IO
    {
        public static bool Copy(string sourceFile, string DestFile)
        {
            try
            {
                Delete(DestFile);
                File.Copy(sourceFile, DestFile);
                return true;
            }
            catch (Exception) { throw; }
        }
        public static bool Copy(string sourceFile)
        {
            return Copy(sourceFile, CreateFileExist(sourceFile));
        }
        public static bool Delete(string path)
        {
            try
            {
                if (File.Exists(path))
                {
                    File.Delete(path);
                    return true;
                }
                else return false;
            }
            catch (Exception) { throw; }
        }
        public static bool Delete(string path, string[] files)
        {
            try
            {
                foreach (var item in files)
                    Delete(path + item);
                //if (File.Exists(path + item))
                //    File.Delete(path + item);
                return true;
                //else return false;
            }
            catch (Exception) { throw; }
        }
        public static bool Delete(FileInfo[] files)
        {
            try
            {
                foreach (var item in files)
                    Delete(item.FullName);
                //if (File.Exists(item.FullName))
                //    File.Delete(item.FullName);
                return true;
                //else return false;
            }
            catch (Exception) { throw; }
        }
        public static bool Delete(FileInfo files)
        {
            try
            {
                Delete(files.FullName);
                //if (File.Exists(files.FullName))
                //    File.Delete(files.FullName);
                return true;
                //else return false;
            }
            catch (Exception) { throw; }
        }
        public static bool DeleteExt(string path, string extension)
        {
            try
            {
                Delete(Files(path, new[] { extension }));
                return true;
            }
            catch (Exception) { throw; }
        }
        public static bool DeleteExt(string path, string[] extension)
        {
            try
            {
                Delete(Files(path, extension));
                return true;
            }
            catch (Exception) { throw; }
        }

        public static bool DeleteDirectory(string path)
        {
            try
            {
                if (Directory.Exists(path))
                {
                    Directory.Delete(path);
                    return true;
                }
                else return false;
            }
            catch (Exception) { throw; }
        }
        public static bool CreateDirectory(string path)
        {
            try
            {
                path = path.Trim('/');
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                    return true;
                }
                return false;
            }
            catch (Exception) { throw; }
        }
        public static string CreateFileExist(string file, params bool[] isServerPath)
        {
            try
            {
                int countFile = 0;
                string extension = Path.GetExtension(file);
                while (File.Exists(file.Substring(0, file.Length - extension.Length) + (countFile > 0 ? "(" + countFile.ToString() + ")" : "") + extension))
                    countFile++;
                file = file.Substring(0, file.Length - extension.Length) + (countFile > 0 ? "(" + countFile.ToString() + ")" : "") + extension;
                return file;
            }
            catch (Exception) { throw; }
        }
        public static byte[] ReturnByteFile(string path)
        {
            try
            {
                byte[] fileBytes = File.ReadAllBytes(path);
                return fileBytes;
            }
            catch (Exception) { throw; }
        }
        public static DirectoryInfo[] Directories(string path)
        {
            try
            {
                //var Directories = Directory.GetDirectories(path);
                var Dir = new DirectoryInfo(path);
                return Dir.GetDirectories();
            }
            catch (Exception) { throw; }
        }
        public static System.Collections.Generic.List<string> DirectoriesToList(string path)
        {
            try
            {
                var list = new System.Collections.Generic.List<string>();
                var subDir = Directories(path);
                foreach (var item in subDir)
                    list.Add(item.Name);
                return list;
            }
            catch (Exception) { throw; }
        }
        public static FileInfo[] Files(string path, string[] extension)
        {
            try
            {
                //var files = System.IO.Directory.GetDirectories(path);
                //string[] ext = new[] { ".dbf" };
                var Dir = new DirectoryInfo(path);
                if (extension != null)
                {
                    for (int i = 0; i < extension.Length; i++)
                        extension[i] = extension[i].ToLower();
                    return Dir.GetFiles().Where(f => extension.Contains(f.Extension.ToLower())).ToArray();
                }
                else
                    return Dir.GetFiles();
                //var subFiles = di.GetFiles("*.dbf").Concat(di.GetFiles("*.dbf2"));
            }
            catch (Exception) { throw; }
        }
        public static FileInfo[] Files(string path)
        {
            return Files(path, null);
        }
        public static System.Collections.Generic.List<string> FilesToList(string path, string[] extension, bool noExtension)
        {
            try
            {
                var list = new System.Collections.Generic.List<string>();
                var subFiles = Files(path, extension);
                foreach (var item in subFiles)
                    if (noExtension)
                        list.Add(item.Name.Replace(item.Extension, ""));
                    else
                        list.Add(item.Name.Replace(item.Extension, item.Extension.ToLower()));
                return list;
            }
            catch (Exception) { throw; }
        }
        //".jpg", ".jpeg", ".png", ".gif", ".tiff", ".bmp"
        public static System.Collections.Generic.List<string> FilesToList(string path, string[] extension)
        {
            return FilesToList(path, extension, false);
        }
        public static System.Collections.Generic.List<string> FilesToList(string path, bool noExtension)
        {
            return FilesToList(path, null, noExtension);
        }
        public static System.Collections.Generic.List<string> FilesToList(string path)
        {
            return FilesToList(path, null, false);
        }
        public static string[] ReadFile(string filename)
        {

            return File.ReadAllLines(filename);
        }
        public static System.Collections.Generic.List<string[]> ReadFile(string filename, char split)
        {
            var rs = new System.Collections.Generic.List<string[]>();
            foreach (var item in ReadFile(filename))
            {
                var tmp = item.Split(split);
                rs.Add(tmp);
            }
            return rs;
        }
        public static string FileDialogFilterImages()
        {
            var codecs = System.Drawing.Imaging.ImageCodecInfo.GetImageEncoders();
            var rs = string.Format("Image files ({0})|{0}|{1}|All files|*",
                string.Join(";", codecs.Select(codec => codec.FilenameExtension).ToArray()),
                string.Join("|", codecs.Select(codec =>
                string.Format("{0} ({1})|{1}", codec.FormatDescription + " files", codec.FilenameExtension)).ToArray()));
            return rs;
        }
        public static string BitmapToBase64(System.Windows.Media.Imaging.BitmapImage BitmapImage)
        {
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            var encoder = new System.Windows.Media.Imaging.PngBitmapEncoder();
            encoder.Frames.Add(System.Windows.Media.Imaging.BitmapFrame.Create(BitmapImage));
            encoder.Save(ms);
            byte[] bitmapdata = ms.ToArray();
            return Convert.ToBase64String(bitmapdata);
        }
        public static byte[] BitmapToByte(System.Windows.Media.Imaging.BitmapImage BitmapImage)
        {
            byte[] data;
            System.Windows.Media.Imaging.JpegBitmapEncoder encoder = new System.Windows.Media.Imaging.JpegBitmapEncoder();
            encoder.Frames.Add(System.Windows.Media.Imaging.BitmapFrame.Create(BitmapImage));
            using (MemoryStream ms = new MemoryStream())
            {
                encoder.Save(ms);
                data = ms.ToArray();
            }
            return data;
        }
        public static System.Windows.Media.Imaging.BitmapImage ByteToBitmap(byte[] imageData)
        {
            if (imageData == null || imageData.Length == 0) return null;
            var image = new System.Windows.Media.Imaging.BitmapImage();
            using (var mem = new MemoryStream(imageData))
            {
                mem.Position = 0;
                image.BeginInit();
                image.CreateOptions = System.Windows.Media.Imaging.BitmapCreateOptions.PreservePixelFormat;
                image.CacheOption = System.Windows.Media.Imaging.BitmapCacheOption.OnLoad;
                image.UriSource = null;
                image.StreamSource = mem;
                image.EndInit();
            }
            image.Freeze();
            return image;
        }
    }
}
public static class IOS
{
    public static string ToExtension(this string file)
    {
        try
        {
            return Path.GetExtension(file);
        }
        catch (Exception) { throw; }
    }
    public static string ToExtensionNone(this string file)
    {
        return ToExtension(file).Trim('.');
    }
    public static bool IsExtension(this string file, string Extension)
    {
        try
        {
            if (file.ToExtension().ToLower() == (Extension[0] == '.' ? Extension.ToLower() : "." + Extension.ToLower()))
                return true;
            else return false;
        }
        catch (Exception) { throw; }
    }
    public static bool IsExtension(this string file, string[] Extension)
    {
        if (Extension.Length > 0)
            foreach (var item in Extension)
                if (file.IsExtension(item)) return true;
        return false;
    }
    public static System.Collections.Generic.List<string> UploadFileSource(this System.Collections.Generic.Dictionary<string, object> Upload)
    {
        try
        {
            return (System.Collections.Generic.List<string>)Upload["UploadFileSource"];
        }
        catch (Exception) { throw; }

    }
    public static string UploadFileSourceString(this System.Collections.Generic.Dictionary<string, object> Upload)
    {
        try
        {
            return (string)Upload["UploadFileSourceString"];
        }
        catch (Exception) { throw; }
    }
    public static System.Collections.Generic.List<string> UploadFile(this System.Collections.Generic.Dictionary<string, object> Upload)
    {
        try
        {
            return (System.Collections.Generic.List<string>)Upload["UploadFile"];
        }
        catch (Exception) { throw; }
    }
    public static string UploadFileString(this System.Collections.Generic.Dictionary<string, object> Upload)
    {
        try
        {
            return (string)Upload["UploadFileString"];
        }
        catch (Exception) { throw; }
    }
    public static System.Collections.Generic.List<string> UploadError(this System.Collections.Generic.Dictionary<string, object> Upload)
    {
        try
        {
            return (System.Collections.Generic.List<string>)Upload["UploadError"];
        }
        catch (Exception) { throw; }
    }
}
