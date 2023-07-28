
"use strict";
const path = require("path");
/*var path = require("path");
var WebpackNotifierPlugin = require("webpack-notifier");
var BrowserSyncPlugin = require("browser-sync-webpack-plugin");
module.exports = {
    entry: ['babel-polyfill', "./React/src/index.js"],
    output: {
        path: path.resolve(__dirname, "./React/dist"),
        filename: "bundle.js"
    },
    resolve: {
        extensions: ['*', '.js', '.jsx']
    },
    module: {
        rules: [
            {
                test: /\.(jsx|js)$/,
                include: path.resolve(__dirname, 'src'),
                exclude: /node_modules/,
                use: [{
                    loader: 'babel-loader',
                    options: {
                        presets: [
                            ['@babel/preset-env', {
                                "targets": "defaults"
                            }],
                            '@babel/preset-react'
                        ]
                    },

                }]
            }]
        ,
    },
    devtool: "inline-source-map",
    devServer: {
        contentBase: path.resolve(__dirname, 'dist'),
        open: true,
        hot: true
    },
    plugins: [new WebpackNotifierPlugin(), new BrowserSyncPlugin(), new webpack.HotModuleReplacementPlugin()],
    resolve: {
        extensions: ['.js', '.jsx'],
    }
}*/


module.exports = {
   
    entry: {
        app: path.resolve(__dirname, "./React/src/index.js"),
    },
    output: {
        filename: "[name].bundle.js",
        path: path.resolve(__dirname, "./Scripts/dist")
    },
    resolve: {
        extensions: [".js", ".jsx"]
    },
    module: {
        rules: [
            {
                test: /\.(jpe?g|png|gif|woff|woff2|otf|eot|ttf|svg)(\?[a-z0-9=.]+)?$/,
                use: [
                    {
                        loader: 'url-loader',
                        options: {
                            limit: 1000,
                            name: 'assets/img/[name].[ext]'
                        }
                    }
                ]
            },
            {
                test: /\.js$|jsx/,
                exclude: /node_modules/,
                use: {
                    loader: "babel-loader"
                }
            }, {
                test: /\.css$/i,
                use: ['style-loader', 'css-loader'],
            }
           
        ]
    },
     devtool: "inline-source-map"
};



    