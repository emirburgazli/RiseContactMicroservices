namespace Contact.Services.Settings
{
    interface IDatabaseSettings
    {
        public string PersonCollectionName { get; set; }
        public string PersonInfoCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}
