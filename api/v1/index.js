const express = require('express');
const router = express.Router();

router.use('/login', require('./login.js'));
router.use('/users', require('./users.js'));
router.use('/accounts', require('./accounts.js'));

module.exports = router;
