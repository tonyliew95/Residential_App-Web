using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Tony Liew 20190328 \/
// Note: The source code below are copy from project GLP
namespace Nop.Services
{
    public static class Config
    {
        public static string AWSS3AccessKey
        {
            get
            {
                return ConfigurationManager.AppSettings["AWSS3AccessKey"];
            }
        }

        public static string AWSS3SecretKey
        {
            get
            {
                return ConfigurationManager.AppSettings["AWSS3SecretKey"];
            }
        }

        public static string Environment
        {
            get
            {
                return ConfigurationManager.AppSettings["Environment"];
            }
        }

        public static string S3ImageUrl
        {
            get
            {
                return ConfigurationManager.AppSettings["S3ImageUrl"]; 
            }
        }

        public static string PictureS3Bucket
        {
            get
            {
                return ConfigurationManager.AppSettings["PictureS3Bucket"];
            }
        }

    }
}
//Tony Liew 20190328 /\