using API.Common.Exceptions;

namespace API.Features.Offers.Exeptions
{
    public class OfferNorFoundExeption : BaseException
    {
        public OfferNorFoundExeption() : base("Oferta nie istnieje")
        {
        }
    }
}
