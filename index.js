const express = require('express');
const config = require('config');
const bodyParser = require('body-parser');

//Initialize DB
require('./db/init.js');

//Configure Express
const app = express();
app.use(bodyParser.json());
app.use(bodyParser.urlencoded({extended: true}));
app.use(function (req, res, next) {
    req.debug = config.get('debug');
    next();
});

//Expose API
app.use('/api', require('./api/index.js'));

//Error handling
app.use(function (err, req, res, next) {
    if (req.debug) {
        console.error(err);
    }
    res.status(500).send("Something went wrong!");
});

//404
app.use(function (req, res, next) {
    res.status(404).send("404: There is nothing here...")
});

//Start server
app.listen(config.get('port'), () => console.log(' +-----\n | Firefly is running and listening on port ' + config.get('port') + '...\n +-----'));
