using API.Common.Exceptions;

namespace API.Features.Offers.Exeptions
{
    public class OfferWithNameExistsExeption : BaseException
    {
        public OfferWithNameExistsExeption() : base("Oferta o takiej nazwie już istnieje")
        { }
    }
}
