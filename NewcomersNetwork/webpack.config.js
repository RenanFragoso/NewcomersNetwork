'use strict';

var webpack = require('webpack');
var path = require('path');

const CopyWebpackPlugin = require('copy-webpack-plugin');
const CleanWebpackPlugin = require('clean-webpack-plugin');
const ExtractTextPlugin = require('extract-text-webpack-plugin');

const cssExtract = new ExtractTextPlugin("css/[name].css", { allChunks: true });
const lessExtract = new ExtractTextPlugin("css/[name].css", { allChunks: true });

var DIST_DIR = path.join(__dirname, './Content/');
var SRC_DIR = path.join(__dirname, './UI/');

var devConfig = function() {

    return {
        name: 'NewcomersNetwork',
        entry: { 
            nnui: SRC_DIR + "nnui.react.jsx",
            nn_adminui: SRC_DIR + "nn_adminui.react.jsx",
        },
        target: 'web',
        output: {
            path: DIST_DIR,
            filename: '[name].js',
            publicPath: '/'
        },
        devtool: 'eval',//'source-map',
        devServer: {
            contentBase: './UI'
        },
        resolve: {
            alias: {
                jquery: "jquery/dist/jquery",
                lib: path.resolve(SRC_DIR, "lib"),
                actions: path.resolve(SRC_DIR, "actions"),
                api: path.resolve(SRC_DIR, "api"),
                reducers: path.resolve(SRC_DIR, "reducers"),
                views: path.resolve(SRC_DIR, "views"),
                store: path.resolve(SRC_DIR, "store")
            },
            extensions: ['.js', '.jsx']
        },
        module: {
            rules: [
                {
                    test: /\.react.jsx?$/,
                    exclude: /node_modules/,
                    loader: 'babel-loader',
                    query: {
                        presets: ['es2015', 'react'],
                        plugins: ["add-module-exports"]
                    }
                },
                {
                    test: /\.js?$/,
                    exclude: /node_modules/,
                    loader: 'babel-loader',
                    query: {
                        presets: ['es2015', 'react'],
                        plugins: ["add-module-exports"]
                    }
                },
                {
                    test: /\.react.js?$/,
                    exclude: /node_modules/,
                    loader: 'babel-loader',
                    query: {
                        presets: ['es2015', 'react'],
                        plugins: ["add-module-exports"]
                    }
                },
                {
                    test: /\.css?$/,
                    loader: cssExtract.extract({ fallback: 'style-loader', use: 'css-loader'})
                },
                {
                    test: /\.less$/,
                    loader: lessExtract.extract({ fallback: 'style-loader', use: ['css-loader', 'less-loader'] })
                },
                {
                    test: /\.(png|svg|jpg|gif)?$/,
                    loader: 'file-loader',
                    query: {
                        name: 'images/[name].[ext]'
                    }
                },
                {
                    test: /\.(woff|woff2|eot|ttf|otf)?$/,
                    loader: 'file-loader',
                    query: {
                        name: 'fonts/[name].[ext]',
                        publicPath: '/Content/'
                    }
                },
                {
                    test: /\.html?$/,
                    loader: 'file-loader',
                    query: {
                        name:'[name].[ext]'
                    }
                }
            ]
        },
        plugins: [
            new CleanWebpackPlugin([ DIST_DIR + '/*'], {root: __dirname}),
            new CopyWebpackPlugin([
                { from: SRC_DIR + 'resources', to: DIST_DIR, ignore: ['**/css/*','**/css/*/*'] }
            ]),
            new webpack.ProvidePlugin({
                $: 'jquery',
                jQuery: 'jquery',
                "window.jQuery": 'jquery',
                React: 'react', 
                Promise: 'es6-promise'
            }),
            cssExtract,
            lessExtract
        ]
    };
}

module.exports = devConfig();