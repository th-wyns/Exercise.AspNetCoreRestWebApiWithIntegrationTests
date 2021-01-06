namespace Users.Models.DTOs
{
    /// <summary>
    /// Representation model for company.
    /// </summary>
    public class CompanyDto
    {
        /// <summary>
        /// The name of the company
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// The catch phrase of the company
        /// </summary>
        public string CatchPhrase { get; set; }
        /// <summary>
        /// The BS of the company.
        /// </summary>
        public string Bs { get; set; }
    }
}
