import axios from 'axios';

axios.defaults.headers.common['Access-Control-Allow-Origin'] = '*';
axios.defaults.headers.common['Access-Control-Allow-Methods'] = 'DELETE, POST, GET, OPTIONS';
axios.defaults.headers.common['Access-Control-Allow-Headers'] = 'Content-Type, Authorization, X-Requested-With';
const instance = axios.create({
    //baseURL: 'https://localhost:7229/'
    baseURL: 'http://motomotoca.com:7066/'
});

export {instance}