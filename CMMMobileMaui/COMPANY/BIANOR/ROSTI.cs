using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using CMMMobileMaui.COMMON;
using CMMMobileMaui.COMPANY.BIANOR.ExtraContent;
using CMMMobileMaui.COMPANY.ExtraContent;
using Microsoft.Maui.Controls;
using Microsoft.Maui;

namespace CMMMobileMaui.COMPANY
{
    public class ROSTI : Company
    {
        public ROSTI():base()
        {
            AddExtraContent(new DeviceLocationContent());
            AddExtraContent(new MouldCalendarContent());
        }

        public override async Task<bool> ScanUserLogin(string code)
        {
            try
            {
                if (!code.StartsWith("u:"))
                {
                    code = RostiDecrypt(code.Replace("-POL", string.Empty));
                }

                string serialNumber = string.Empty;

#if ANDROID

                serialNumber = DependencyService.Get<ISerialNumberService>()
                    .GetSerialNumber();
#endif

                var personResponse = await identityController.LoginCode(new API.Contracts.v1.Requests.Identity.LoginCodeRequest
                {
                    Code = code
                    , Mobile_Info = serialNumber
                });

                if (personResponse.IsValid)
                {
                    API.MainObjects.Instance.CurrentUser = personResponse.Data;

                    return true;
                }                           
            }
            catch (Exception) { }

            return false;
        }

        public override async Task<bool> SaveUserLogin(string code)
        {
            try
            {
                string str = RostiDecrypt(code.Replace("-POL", string.Empty));

                if (!string.IsNullOrEmpty(str))
                {
                    var updateResponse = await personBLL.UpdateCode(new API.Contracts.v1.Requests.User.UpdateUserCodeRequest
                    {
                        PersonID = API.MainObjects.Instance.CurrentUser.PersonID
                        , Code = str
                    });

                    if (updateResponse.IsValid
                        && updateResponse.Data)
                    {
                        return true;
                    }
                }
            }
            catch (Exception) { }

            return false;
        }

        private string RostiDecrypt(string text)
        {
            byte[] byteSrc = StringToByteArray(text.Substring(0, text.Length - 96));
            byte[] byteNewIV = StringToByteArray(text.Substring(text.Length - 96, 32));
            byte[] byteKey = StringToByteArray(text.Substring(text.Length - 64, 64));

            byte[] byteTemp = new byte[byteNewIV.Length];

            Array.Copy(byteKey, byteTemp, byteKey.Length / 2);
            byte[] byteIV = new byte[byteNewIV.Length];

            for (int i = 0; i < byteNewIV.Length; ++i)
            {
                byteIV[i] = (byte)(byteNewIV[i] ^ byteTemp[i]);
            }

            using (AesCryptoServiceProvider aes = new AesCryptoServiceProvider())
            {
                aes.Mode = CipherMode.CBC;
                aes.Key = byteKey;
                aes.IV = byteIV;

                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, aes.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(byteSrc, 0, byteSrc.Length);
                        cs.FlushFinalBlock();
                        byte[] byteDescrypt = ms.ToArray();
                        return Encoding.UTF8.GetString(byteDescrypt);
                    }
                }
            }
        }

        private string ByteArrayToString(byte[] ba)
        {
            return BitConverter.ToString(ba).Replace("-", "");
        }

        private  byte[] StringToByteArray(string hex)
        {
            int NumberChars = hex.Length;
            byte[] bytes = new byte[NumberChars / 2];
            for (int i = 0; i < NumberChars; i += 2)
                bytes[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);
            return bytes;
        }
    }
}
