import axios from 'axios';
axios.defaults.headers.common['Access-Control-Allow-Origin'] = '*';

const VPICApi = axios.create({
    baseURL: 'https://vpic.nhtsa.dot.gov/api/vehicles'
});

const PersonalizedRecsApi = axios.create({
    baseURL: 'https://motomoto.ca:7167/Api'
});

export {VPICApi, PersonalizedRecsApi}