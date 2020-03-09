using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net;
using System.Drawing;

namespace ProjetoN9Aula.Classes
{
    public class FTP
    {
        //private static string _usuarioFTP = "admin", _senhaFTP = "123", _servidorFTP = "ftp://192.168.15.10";


        public static string UsuarioFTP { get; set; } = "admin";

        public static string SenhaFTP { get; set; } = "123";

        public static string ServidorFTP { get; set; } = "ftp://192.168.15.10";

        //public FTP()
        //{
        //    _usuarioFTP = "admin";
        //    _senhaFTP = "123456";
        //    _servidorFTP = "ftp://192.168.1.16";
        //}
        //public FTP(string usuarioFTP, string senhaFTP, string servidorFTP)
        //{
        //    _usuarioFTP = usuarioFTP;
        //    _senhaFTP = senhaFTP;
        //    _servidorFTP = servidorFTP;
        //}


        /// <summary>
        /// Upload de arquivos
        /// </summary>
        /// <param name="arquivo"></param>
        /// <param name="url"></param>
        /// <param name="usuario"></param>
        /// <param name="senha"></param>
        public static void EnviarArquivoFTP(string arquivo, string url, string usuario, string senha)
        {
            try
            {
                FileInfo arquivoInfo = new FileInfo(arquivo);

                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(new Uri(url));

                request.Method = WebRequestMethods.Ftp.UploadFile;
                request.Credentials = new NetworkCredential(usuario, senha);
                request.UseBinary = true;
                request.ContentLength = arquivoInfo.Length;

                using (FileStream fs = arquivoInfo.OpenRead())
                {
                    byte[] buffer = new byte[2048];
                    int bytesSent = 0;
                    int bytes = 0;

                    using (Stream stream = request.GetRequestStream())
                    {
                        while (bytesSent < arquivoInfo.Length)
                        {
                            bytes = fs.Read(buffer, 0, buffer.Length);
                            stream.Write(buffer, 0, bytes);
                            bytesSent += bytes;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Download de arquivos
        /// </summary>
        /// <param name="url"></param>
        /// <param name="local"></param>
        /// <param name="usuario"></param>
        /// <param name="senha"></param>
        public static void BaixarArquivoFTP(string url, string local, string usuario, string senha)
        {
            try
            {
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(url);
                request.Method = WebRequestMethods.Ftp.DownloadFile;
                request.Credentials = new NetworkCredential(usuario, senha);
                request.UseBinary = true;

                using (FtpWebResponse response = (FtpWebResponse)request.GetResponse())
                {
                    using (Stream rs = response.GetResponseStream())
                    {
                        using (FileStream ws = new FileStream(local, FileMode.Create))
                        {
                            byte[] buffer = new byte[2048];
                            int bytesRead = rs.Read(buffer, 0, buffer.Length);

                            while (bytesRead > 0)
                            {
                                ws.Write(buffer, 0, bytesRead);
                                bytesRead = rs.Read(buffer, 0, buffer.Length);
                            }
                        }
                    }
                }
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// string _ftpURL = "testftp.com";     //Host URL or address of the FTP server
        /// string _UserName = "admin";         //User Name of the FTP server
        /// string _Password = "admin123";      //Password of the FTP server
        /// string _ftpDirectory = "Receipts";  //The directory in FTP server where the files will be uploaded
        /// string _FileName = "test1.csv";     //File name, which one will be uploaded
        /// DeleteFile(_ftpURL, _UserName, _Password, _ftpDirectory, _FileName);
        /// </summary>
        /// <param name="ftpURL"></param>
        /// <param name="UserName"></param>
        /// <param name="Password"></param>
        /// <param name="ftpDirectory"></param>
        /// <param name="FileName"></param>
        public void DeletaArquivo(string ftpURL, string UserName, string Password, string ftpDirectory, string FileName)
        {
            try
            {
                FtpWebRequest ftpRequest = (FtpWebRequest)WebRequest.Create(ftpURL + "/" + ftpDirectory + "/" + FileName);
                ftpRequest.Credentials = new NetworkCredential(UserName, Password);
                ftpRequest.Method = WebRequestMethods.Ftp.DeleteFile;
                FtpWebResponse responseFileDelete = (FtpWebResponse)ftpRequest.GetResponse();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// string _ftpURL = "testftp.com";         //Host URL or address of the FTP server
        /// string _UserName = "admin";             //User Name of the FTP server
        /// string _Password = "admin123";          //Password of the FTP server
        /// string _ftpDirectory = "Receipts";      //The directory in FTP server where the file will be uploaded
        /// string _FileName = "test1.csv";         //File name, which one will be uploaded
        /// string _ftpDirectoryProcessed = "Done"; //The directory in FTP server where the file will be moved
        /// MoveFile(_ftpURL, _UserName, _Password, _ftpDirectory, _FileName, _ftpDirectoryProcessed);
        /// </summary>
        /// <param name="ftpURL"></param>
        /// <param name="UserName"></param>
        /// <param name="Password"></param>
        /// <param name="ftpDirectory"></param>
        /// <param name="ftpDirectoryProcessed"></param>
        /// <param name="FileName"></param>
        public void MoverArquivo(string ftpURL, string UserName, string Password, string ftpDirectory, string ftpDirectoryProcessed, string FileName)
        {
            FtpWebRequest ftpRequest = null;
            FtpWebResponse ftpResponse = null;
            try
            {
                ftpRequest = (FtpWebRequest)WebRequest.Create(ftpURL + "/" + ftpDirectory + "/" + FileName);
                ftpRequest.Credentials = new NetworkCredential(UserName, Password);
                ftpRequest.UseBinary = true;
                ftpRequest.UsePassive = true;
                ftpRequest.KeepAlive = true;
                ftpRequest.Method = WebRequestMethods.Ftp.Rename;
                ftpRequest.RenameTo = ftpDirectoryProcessed + "/" + FileName;
                ftpResponse = (FtpWebResponse)ftpRequest.GetResponse();
                ftpResponse.Close();
                ftpRequest = null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// string _ftpURL = "testftp.com";             //Host URL or address of the FTP server
        /// string _UserName = "admin";                 //User Name of the FTP server
        /// string _Password = "admin123";              //Password of the FTP server
        /// string _ftpDirectory = "Receipts";          //The directory in FTP server where the files are present
        /// string _FileName = "test1.csv";             //File name, which one will be downloaded
        /// string _LocalDirectory = "D:\\FilePuller";  //Local directory where the files will be downloaded
        /// DownloadFile(_ftpURL, _UserName, _Password, _ftpDirectory, _FileName, _LocalDirectory);
        /// </summary>
        /// <param name="ftpURL"></param>
        /// <param name="UserName"></param>
        /// <param name="Password"></param>
        /// <param name="ftpDirectory"></param>
        /// <param name="FileName"></param>
        /// <param name="LocalDirectory"></param>
        public void DownloadFile(string ftpURL, string UserName, string Password, string ftpDirectory, string FileName, string LocalDirectory)
        {
            if (!File.Exists(LocalDirectory + "/" + FileName))
            {
                try
                {
                    FtpWebRequest requestFileDownload = (FtpWebRequest)WebRequest.Create(ftpURL + "/" + ftpDirectory + "/" + FileName);
                    requestFileDownload.Credentials = new NetworkCredential(UserName, Password);
                    requestFileDownload.Method = WebRequestMethods.Ftp.DownloadFile;
                    FtpWebResponse responseFileDownload = (FtpWebResponse)requestFileDownload.GetResponse();
                    Stream responseStream = responseFileDownload.GetResponseStream();
                    FileStream writeStream = new FileStream(LocalDirectory + "/" + FileName, FileMode.Create);
                    int Length = 2048;
                    Byte[] buffer = new Byte[Length];
                    int bytesRead = responseStream.Read(buffer, 0, Length);
                    while (bytesRead > 0)
                    {
                        writeStream.Write(buffer, 0, bytesRead);
                        bytesRead = responseStream.Read(buffer, 0, Length);
                    }
                    responseStream.Close();
                    writeStream.Close();
                    requestFileDownload = null;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        //Feito por mim - Rafael
        public static byte[] GetImgByte(string nomeFoto)
        {
            WebClient ftpClient = new WebClient();
            ftpClient.Credentials = new NetworkCredential(UsuarioFTP, SenhaFTP);

            byte[] imageByte = ftpClient.DownloadData(ServidorFTP + "/" + nomeFoto);
            return imageByte;
        }

        //public static byte[] GetImgByte(string ftpFilePath)
        //{
        //    WebClient ftpClient = new WebClient();
        //    ftpClient.Credentials = new NetworkCredential(UsuarioFTP, SenhaFTP);

        //    byte[] imageByte = ftpClient.DownloadData(ftpFilePath);
        //    return imageByte;
        //}

        //public static Bitmap ByteToImage(byte[] blob)
        //{
        //    MemoryStream mStream = new MemoryStream();
        //    byte[] pData = blob;
        //    mStream.Write(pData, 0, Convert.ToInt32(pData.Length));
        //    Bitmap bm = new Bitmap(mStream, false);
        //    mStream.Dispose();
        //    return bm;
        //}
    }
}

