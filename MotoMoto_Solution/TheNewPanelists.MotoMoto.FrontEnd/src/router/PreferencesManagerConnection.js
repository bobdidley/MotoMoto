import axios from 'axios';

axios.defaults.headers.common['Access-Control-Allow-Origin'] = '*';

const instance = axios.create({
    baseURL: 'https://localhost:7167/Api'
});

export {instance}