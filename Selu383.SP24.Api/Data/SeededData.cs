namespace Selu383.SP24.Api.Data
{
    public class SeededData
    {
        public SeededData(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
    }
}
