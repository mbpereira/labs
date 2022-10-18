require('dotenv/config')

const ceps = [
    {
        "cep": "01001-000",
        "logradouro": "Praça da Sé",
        "complemento": "lado ímpar",
        "bairro": "Sé",
        "localidade": "São Paulo",
        "uf": "SP",
        "ibge": "3550308",
        "gia": "1004",
        "ddd": "11",
        "siafi": "7107"
    },
    {
        "cep": "78058-160",
        "logradouro": "Rua Chupim",
        "complemento": "(Núc Hab CPA IV)",
        "bairro": "Morada da Serra",
        "localidade": "Cuiabá",
        "uf": "MT",
        "ibge": "5103403",
        "gia": "",
        "ddd": "65",
        "siafi": "9067"
    },
    {
        "cep": "78085-630",
        "logradouro": "Avenida Santo Antônio",
        "complemento": "",
        "bairro": "Coxipó",
        "localidade": "Cuiabá",
        "uf": "MT",
        "ibge": "5103403",
        "gia": "",
        "ddd": "65",
        "siafi": "9067"
    }
]

const express = require('express')

const app = express()

app.use((req, res, next) => {
    const key = req.headers['x-token']
    
    if(key !== process.env.XTOKEN)
        return next({ "error": "invalid token" })

    return next()
})

app.use((err, req, res, next) => {
    res.status(500).send(err)
})

app.get('/ceps', (req, res) => {
    return res.status(200).send(ceps)
})

app.get('/ceps/:cep', (req, res) => {
    const { cep } = req.params
    const foundCep = ceps.find(c => c.cep === cep)

    if(!foundCep) {
        return res.status(404).send({
            "message": "não encontrado"
        })
    }

    return res.status(200).send(foundCep)
})

app.listen(process.env.PORT || 500, () => {
    console.log(`listening on ${process.env.PORT}`)
})