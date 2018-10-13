const express = require('express');
const router = express.Router();

//Authorization middleware
let authorize = require('../../middleware/authorize.js');

//Login route, publicly available
router.use('/login', require('./login.js'));

//Protected routes
router.use('/users', authorize, require('./users.js'));
router.use('/accounts', authorize, require('./accounts.js'));

module.exports = router;
