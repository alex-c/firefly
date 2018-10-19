const express = require('express');
const router = express.Router();

//Authorization middleware
let authorizeUser = require('./middleware/authorizeUser.js');

//Login route, publicly available
router.use('/login', require('./login.js'));

//Protected routes
router.use('/users', authorizeUser, require('./users.js'));
router.use('/accounts', authorizeUser, require('./accounts.js'));

module.exports = router;
