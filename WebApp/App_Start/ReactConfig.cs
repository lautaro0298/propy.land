using JavaScriptEngineSwitcher.Core;
using JavaScriptEngineSwitcher.V8;
using React;


[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(WebApp.ReactConfig), "Configure")]

namespace WebApp
{
	public static class ReactConfig
	{
		public static void Configure()
		{
			// If you want to use server-side rendering of React components, 
			// add all the necessary JavaScript files here. This includes 
			// your components as well as all of their dependencies.
			// See http://reactjs.net/ for more information. Example:
			//ReactSiteConfiguration.Configuration
			//	.AddScript("../Scripts/First.jsx")
			//	.AddScript("../Scripts/Second.jsx");
			
			JsEngineSwitcher.Current.DefaultEngineName = V8JsEngine.EngineName;
			JsEngineSwitcher.Current.EngineFactories.AddV8();
			ReactSiteConfiguration.Configuration
				.SetLoadBabel(false)
				.AddScript("~/React/src/components/Checkout-Page/CheckoutPage.jsx")
				.AddScript("~/React/src/components/Context/SearchDataContext.jsx")
				.AddScript("~/React/src/components/FilterSearchBar/FilterSearchBar.jsx")
				.AddScript("~/React/src/components/footer/Footer.jsx")
				.AddScript("~/React/src/components/home-page-carousel component/HomeCarousel.jsx")
				.AddScript("~/React/src/components/home-page-carousel component/ModifielCarousel.jsx")
				.AddScript("~/React/src/components/home-page-carousel component/TopCities.jsx")
				.AddScript("~/React/src/components/home-page-carousel component/TopDestinations.jsx")
				.AddScript("~/React/src/components/Home page/Home.jsx")
				.AddScript("~/React/src/components/Home page/MoreCitiesSection.jsx")
				.AddScript("~/React/src/components/Home page/QNASection.jsx")
				.AddScript("~/React/src/components/HotDet/Amenity.jsx")
				.AddScript("~/React/src/components/HotDet/BigHot.jsx")
				.AddScript("~/React/src/components/HotDet/clean.jsx")
				.AddScript("~/React/src/components/HotDet/ComparePrice.jsx")
				.AddScript("~/React/src/components/HotDet/Deal.jsx")
				.AddScript("~/React/src/components/HotDet/HotCard.jsx")
				.AddScript("~/React/src/components/HotDet/OverBelow.jsx")
				.AddScript("~/React/src/components/HotDet/Photo.jsx")
				.AddScript("~/React/src/components/HotDet/Reviw.jsx")
				.AddScript("~/React/src/components/Hotel Page/HotelPage.jsx")
				.AddScript("~/React/src/components/Hotel Page/PaginationComponent.jsx")
				.AddScript("~/React/src/components/Hotel Page/SortBy.jsx")
				.AddScript("~/React/src/components/Image SlideShow/ImageSliderData.js")
				.AddScript("~/React/src/components/Image SlideShow/ImageSliderShow.jsx")
				.AddScript("~/React/src/components/login-signup component/Auth.jsx")
				.AddScript("~/React/src/components/login-signup component/AuthRoute.jsx")
				.AddScript("~/React/src/components/login-signup component/Login.jsx")
				.AddScript("~/React/src/components/login-signup component/Signup.jsx")
				.AddScript("~/React/src/components/login-signup component/Auth.jsx")
				.AddScript("~/React/src/components/Map/Map.jsx")
				.AddScript("~/React/src/components/Map/MapComponent.jsx")
				.AddScript("~/React/src/components/Map/MultiMap.jsx")
				.AddScript("~/React/src/components/Map/StaticMap.jsx")
				.AddScript("~/React/src/components/material-ui-components/GuestCard.jsx")
				.AddScript("~/React/src/components/material-ui-components/GuestCardAnimate.css")
				.AddScript("~/React/src/components/material-ui-components/LoadingAnimation.jsx")
				.AddScript("~/React/src/components/material-ui-components/LocationCard.jsx")
				.AddScript("~/React/src/components/material-ui-components/MoreFilterCard.jsx")
				.AddScript("~/React/src/components/material-ui-components/MuiSlider.jsx")
				.AddScript("~/React/src/components/material-ui-components/MuiTabs.jsx")
				.AddScript("~/React/src/components/material-ui-components/RatingCard.jsx")
				.AddScript("~/React/src/components/navbar/NavBar.jsx")
				.AddScript("~/React/src/components/navbar/ShowProfile.jsx")
				.AddScript("~/React/src/components/Recently-activity/RecentlyData.jsx")
				.AddScript("~/React/src/components/Recently-activity/AccountSetting.jsx")
				.AddScript("~/React/src/components/Recently-activity/BookingOverview.jsx")
				.AddScript("~/React/src/components/Recently-activity/RecentlyContent.jsx")
				.AddScript("~/React/src/components/Recently-activity/RecentlyHome.jsx")
				.AddScript("~/React/src/components/Recently-activity/RecentlyMain.jsx")
				.AddScript("~/React/src/components/Recently-activity/RecentlyViewed.jsx")
				.AddScript("~/React/src/components/Redirect page/Redirect.jsx")
				.AddScript("~/React/src/components/Route component/Routes.jsx")
				.AddScript("~/React/src/components/Search-Bar/Search.jsx")
				.AddScript("~/React/src/components/Search-Bar/SearchBar.jsx")
				.AddScript("~/React/src/components/Search-Bar/dummy.jsx")
				.AddScript("~/React/src/components/Search-Bar/Search.css")
				.AddScript("~/React/src/App default css/App.css")
				.AddScript("~/React/src/store/login/actions.jsx")
				.AddScript("~/React/src/store/login/actionsTypes.jsx")
				.AddScript("~/React/src/store/login/Reducer.jsx")
				.AddScript("~/React/src/store/Redirect/actions.jsx")
				.AddScript("~/React/src/store/Redirect/actionsTypes.jsx")
				.AddScript("~/React/src/store/Redirect/Reducer.jsx")
				.AddScript("~/React/src/store/Redirect/Store.jsx")
				.AddScript("~/React/src/store/actions.jsx")
				.AddScript("~/React/src/store/actionsTypes.jsx")
				.AddScript("~/React/src/store/Reducer.jsx")
				.AddScript("~/React/src/store/Store.jsx")
				.AddScript("~/React/src/utils/LocalStorage.jsx")
				.AddScript("~/React/App.js")
				.AddScript("~/React/index.jsx");

			// If you use an external build too (for example, Babel, Webpack,
			// Browserify or Gulp), you can improve performance by disabling 
			// ReactJS.NET's version of Babel and loading the pre-transpiled 
			// scripts. Example:
			//ReactSiteConfiguration.Configuration
			//	.SetLoadBabel(false)
			//	.AddScriptWithoutTransform("../Scripts/bundle.server.js")
		}
	}
}