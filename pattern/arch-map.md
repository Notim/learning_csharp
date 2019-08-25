## Structure Map
root/
    config/  -> App start, Have Global config and setup files
        env/
            dev.env
            prod.env
            stag.env
        middleware.js
        routes.js        
        server.js
    docs/ -> where your documentation stay
            arch-map.md
    infra/ -> App infrastructure, have global resources to all apps
        caching/ -> caching tools
        extensions/ -> conversions type tools
        helpers/ -> to make view helpers extensions
        filters/ -> app filters for permission or other
        logging/ -> app logger
        securing/ -> securing protocols using in app
    modules/ -> domains of your app
        news/ -> news domain
            assets/ -> Domain public resources
                css/ -> domain custom stylesheet
                img/ -> domain images
                fonts/ -> domain custom fonts
                js/ -> domain custom js
                libs/ -> domain custom libs needs
            controllers/ -> controllers stay here 
                newsListing.js
            models/ -> domain objects stay here
                entities/ -> domain objects stay here
                repositories/ -> Data access from entities stay here
                fillers/ -> viewModel filler for only consulting
                forms/ -> objects for persist data
                validators -> validate form before persist
                viewModels/ -> view object
            routes/ -> where you configure module routes
                routes.js
            views/ -> where views stay XD
                newsListing/ -> action name represent
                     index.ejs
                     listing.ejs
    assets/ -> Global public resources
        css/ -> Global stylesheet
        img/ -> Global images
        fonts/ -> Global fonts
        js/ -> Global js
        libs/ -> Global libs needs
    tests/ -> your tests stay here
    views/ -> Global views
        partials/
            top-menu.ejs
        master-page.ejs
    app.js -> app start
    package.json