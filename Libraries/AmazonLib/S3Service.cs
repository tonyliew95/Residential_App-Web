using Amazon.S3;
using Amazon.S3.Model;
using System.IO;
//Tony Liew 20190328 \/
namespace AmazonLib
{
    public class S3Service
    {

        public string AccessKey { get; set; }
        public string SecretKey { get; set; }
        public string BucketName { get; set; }
        public string Key { get; set; }
        
        public S3Service(string accessKey, string secretKey, string bucketName, string key)
        {
            AccessKey = accessKey;
            SecretKey = secretKey;
            BucketName = bucketName;
            Key = key;
        }

        public bool IsExist()
        {
            AmazonS3Client _s3Client = new AmazonS3Client(AccessKey, SecretKey, Amazon.RegionEndpoint.USEast1);

            try
            {

                var a = new GetObjectMetadataRequest();
                a.BucketName = BucketName;
                a.Key = Key;

                
                var ss = _s3Client.GetObjectMetadata(a);

                return true;
            }

            catch (Amazon.S3.AmazonS3Exception ex)
            {
                if (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
                    return false;

                //status wasn't not found, so throw the exception
                throw;
            }

        }

        public void UploadFile(string path)
        {
            AmazonS3Client dsa = new AmazonS3Client(AccessKey, SecretKey, Amazon.RegionEndpoint.USEast1);

            using (FileStream file = new FileStream(path, FileMode.Open))
            {
                dsa.PutObject(new PutObjectRequest()
                {
                    BucketName = BucketName,
                    InputStream = (Stream)file,
                    Key = Key,
                    CannedACL = S3CannedACL.PublicRead
                });
            }
        }

        public void UploadFile(Stream stream)
        {
            AmazonS3Client client = new AmazonS3Client(AccessKey, SecretKey, Amazon.RegionEndpoint.USEast1);
            
            client.PutObject(new PutObjectRequest()
            {
                BucketName = BucketName,
                InputStream = stream,
                Key = Key,
                CannedACL = S3CannedACL.PublicRead
            });

        }

    }
}
//Tony Liew 20190328 /\
