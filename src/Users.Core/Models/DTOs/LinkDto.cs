namespace Users.Models.DTOs
{
    /// <summary>
    /// HATEOAS link representation.
    /// </summary>
    public class LinkDto
    {
        /// <summary>
        /// The URI to the resource.
        /// </summary>
        public string Href { get; set; }
        /// <summary>
        /// The action name.
        /// </summary>
        public string Rel { get; set; }
        /// <summary>
        /// The HTTP verb to use.
        /// </summary>
        public string Method { get; set; }

        public LinkDto()
        {
        }

        public LinkDto(string href, string rel, string method)
        {
            Href = href;
            Rel = rel;
            Method = method;
        }
    }
}
