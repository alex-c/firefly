if (process.argv.length == 4) {

    let user = {
        name: process.argv[2],
        password: process.argv[3],
        is_admin: true
    }

    console.log(`Attempting to create admin user '${user.name}' with password '${user.password}'.`);

    //Init DB connection
    require('../db/init.js');

    //Get user model
    const User = require('../models/User.js');

    //Create admin users
    (async function() {
        await User.query().insert(user);
        console.log(`Admin user '${user.name}' successfully created.`);
        process.exit();
    })();

} else {
    console.error("Insufficient parameters. Call: node create_admin.js <name> <password>");
}
