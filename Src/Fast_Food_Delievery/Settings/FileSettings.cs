namespace Fast_Food_Delievery.Settings
{
    public class FileSettings
    {
        public const string ImagesPath = "/Assets/Images/Items";

        public const string AllowedExtensions = ".jpg,.jpeg,.png";

        public const int MaxFileSizeInMB = 1;
        public const int MaxFileSizeInBytes = MaxFileSizeInMB * 1024 * 1024;
    }
}
