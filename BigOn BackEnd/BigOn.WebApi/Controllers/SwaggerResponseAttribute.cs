namespace BigOn.WebApi.Controllers
{
    internal class SwaggerResponseAttribute : Attribute
    {
        private int status201Created;

        public SwaggerResponseAttribute(int status201Created)
        {
            this.status201Created = status201Created;
        }
    }
}