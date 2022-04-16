namespace Contact.Services.Settings
{
   public interface IDatabaseSettings
    {
        public string PersonCollectionName { get; set; }
        public string PersonInfoCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}
