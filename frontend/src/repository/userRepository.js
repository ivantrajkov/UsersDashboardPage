import axiosInstance from "../axios/axios.js"

const userRepository = {
    findAll: async () => {
        return await axiosInstance.get("/User/all")
    },
    delete: async(username) => {
        return await axiosInstance.delete(`/User/delete/${username}`)
    },
    register: async(data) => {
        return await axiosInstance.post("/User/register", data)
    },
    login: async(data) => {
        return await axiosInstance.post(`User/login`, data)
    }

    
}
export default userRepository;