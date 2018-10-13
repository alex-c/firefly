const jwt = require('jsonwebtoken');
const config = require('config');

//Express middleware that authorizes a route using the JWT token supplied in the authorization header
function authorize(req, res, next) {

    var authHeader = req.headers['authorization'];

    if (authHeader) {

        //Verify token
        var token = authHeader.substr(authHeader.indexOf(" ") + 1);
        jwt.verify(token, config.get('secret'), function(error, decoded) {
            if (error) {
                res.status(401).end();
            } else {
                req.token = decoded;
                next();
            }
        });

    } else {
        res.status(401).end();
    }
}

module.exports = AuthModule;
