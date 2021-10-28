module.exports = {
    module: {
        rules : [
            // Esta es la regla para procesar archivos css
            {
                test: /\.(scss)/,
                use: [
                    {
                        loader: "style-loader"
                    },
                    {
                        loader: "css-loader"
                    },
                    {
                        loader: "sass-loader"
                    }
                ]
            }
        ]
    }
}