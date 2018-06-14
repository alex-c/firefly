const express = require('express');
const config = require('config');
const bodyParser = require('body-parser');

//Configure Express
const app = express();
app.use(bodyParser.json());
app.use(bodyParser.urlencoded({extended: true}));

//Test route
app.get("", function(req, res) {
    res.json({success: true});
});

//Error handling
app.use(function (err, req, res, next) {
    res.status(500).send("Error!");
});

//404
app.use(function (req, res, next) {
    res.status(404).send("404: There is nothing here...")
});

//Start server
app.listen(config.get('port'), () => console.log(' +-----\n | Firefly is running and listening on port ' + config.get('port') + '...\n +-----'));
