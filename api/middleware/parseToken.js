const jwt = require('jsonwebtoken');
const config = require('config');

//Express middleware that authorizes a route using the JWT token supplied in the authorization header
function parseToken(req, res, next) {

    var authHeader = req.headers['authorization'];

    if (authHeader !== undefined) {

        //Verify token
        var token = authHeader.substr(authHeader.indexOf(" ") + 1);
        jwt.verify(token, config.get('secret'), function(error, decoded) {
            if (error) {
                req.token = null;
            } else {
                req.token = decoded;
            }
            next();
        });

    } else {
        res.token = null;
        next();
    }
}

module.exports = parseToken;
