using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace ProjetoN9Aula.Classes
{

    public static class Imagem
    {

        private static string _servidorImagensSite = "ftp://"+FTP.UsuarioFTP+":"+FTP.SenhaFTP+"@192.168.15.10/";

        public static string PegarCaminhoRealUri(Android.Net.Uri contentURI, Context contexto)
        {
            try
            {
                Android.Database.ICursor cursor = contexto.ContentResolver.Query(contentURI, null, null, null, null);
                if (cursor != null)
                {
                    cursor.MoveToFirst();
                    string documentId = cursor.GetString(0);
                    documentId = documentId.Split(':')[0];
                    cursor.Close();
                    cursor = contexto.ContentResolver.Query(Android.Provider.MediaStore.Images.Media.ExternalContentUri,
                                                   null,
                                                   Android.Provider.MediaStore.Images.Media.InterfaceConsts.Id + " = ? ", new[] { documentId },
                                                   null);
                    cursor.MoveToFirst();
                    string path = cursor.GetString(cursor.GetColumnIndex(Android.Provider.MediaStore.Images.ImageColumns.Data));
                    cursor.Close();
                    return path;
                }
                return contentURI.Path;
            }
            catch (Android.Database.CursorIndexOutOfBoundsException ex)
            {
                throw ex;
            }
        }

        public static Bitmap PegarImagemBitmapPorUrl(string url)
        {
            Bitmap imageBitmap = null;
            try
            {
                using (var webClient = new WebClient())
                {
                    var imageBytes = webClient.DownloadData(url);
                    if (imageBytes != null && imageBytes.Length > 0)
                    {
                        imageBitmap = BitmapFactory.DecodeByteArray(imageBytes, 0, imageBytes.Length);
                    }
                }
                return imageBitmap;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static Bitmap RecuperarImagemBitmap(string nomeImagem)
        {
            Bitmap imageBitmap = null;
            try
            {
                using (var webClient = new WebClient())
                {
                    var imageBytes = webClient.DownloadData(_servidorImagensSite + nomeImagem);
                    if (imageBytes != null && imageBytes.Length > 0)
                    {
                        imageBitmap = BitmapFactory.DecodeByteArray(imageBytes, 0, imageBytes.Length);
                    }
                }
                return imageBitmap;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static byte[] RecuperarImagemByte(string nomeImagem)
        {
            byte[] imageBytes;
            try
            {
                using (var webClient = new WebClient())
                {
                    imageBytes = webClient.DownloadData(_servidorImagensSite + nomeImagem); 
                }
                return imageBytes;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static byte[] BitmapToByteArray(Bitmap bitmap)
        {

            byte[] bitmapData;
            using (var stream = new MemoryStream())
            {
                bitmap.Compress(Bitmap.CompressFormat.Png, 0, stream);
                bitmapData = stream.ToArray();
            }
            return bitmapData;
        }
    }
}