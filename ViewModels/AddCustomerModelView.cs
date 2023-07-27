using Vidly.Models;

namespace Vidly.ViewModels
{
    public class AddCustomerModelView
    {
        public Customer Customer { get; set; }
        public IEnumerable<MembershipType> MembershipTypes { get; set; } = Enumerable.Empty<MembershipType>();
    }
}
