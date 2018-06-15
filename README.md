# Firefly

> Keep flying by budgeting and tracking expenses!

Firefly is a simple, free and open source budgeting and expense tracking application. It is intended for personal use, and supports multiple users and accounts.

Example use cases:

* Track your personal expenses
* Track your and your partners expenses, and let your children manage their pocket money
* Budget and track expenses of a group project

## Status

Please note that Firefly is in early development!

## Getting started

Firefly requires [Node.js](https://nodejs.org/en/) and a [PostgreSQL](https://www.postgresql.org/) database to run. First you need to download Firefly, and then install it with npm.

### Download from Github

You can download Firefly from Github! Click on "Clone or download" and then select "Download ZIP". Then extract the ZIP to where you want Firefly to run.

### Download from NPM

Downloading Firefly through NPM is not available yet. This will follow when Firefly is more mature!

### Install

To install Firefly, navigate to the directory where you downloaded it to, and install using NPM:

```bash
npm install
```

### Run

To run Firefly, run the `index.js` script with Node:

```bash
node index.js
```

### Configuration

Configuration files are located in the `config` directory. A `default.json` file should already be there. You can edit it, or add a `local.json` file. Values from the local configuration file will override those from the default configuration file (see the [documentation of the config module](https://github.com/lorenwest/node-config) for more information!).
