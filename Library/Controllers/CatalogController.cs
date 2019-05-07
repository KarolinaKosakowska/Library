using Library.Models.Catalog;
using LibraryData;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Library.Controllers
{
    public class CatalogController: Controller
    {
        private readonly ILibraryAsset assets;

        public CatalogController(ILibraryAsset assets)
        {
            this.assets = assets;
        }
        public IActionResult Index()
        {
            var assetModels = assets.GetAll();
            var listingResult = assetModels
                .Select(result => new AssetIndexListingModel
                {
                    Id = result.Id,
                    ImageUrl=result.ImageUrl,
                    AuthorOrDirector=assets.GetAuthorOrDirector(result.Id),
                    DeweyCallNumber=assets.GetDeweyIndex(result.Id),
                    Title=result.Title,
                    Type=assets.GetType(result.Id)




            } );
        }
    }
}
