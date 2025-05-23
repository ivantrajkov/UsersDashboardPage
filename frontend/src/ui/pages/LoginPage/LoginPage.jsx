import { useNavigate } from "react-router-dom"
import { useState } from "react";
import userRepository from "../../../repository/userRepository.js"
import useAuth from "../../../hooks/useAuth.js"
import React from "react";
const initialFormData = {
    "username": "",
    "password": "",
};


const LoginPage = () => {
    const navigate = useNavigate();

    const [formData, setFormData] = useState(initialFormData);

    const { login } = useAuth();
    const [correct, setCorrect] = useState(true)

    const handleChange = (event) => {
        const { name, value } = event.target;
        setFormData({ ...formData, [name]: value })
    }

    const handleSubmit = () => {
        userRepository
            .login(formData)
            .then((response) => {
                setCorrect(true)
                login(response.data.accessToken)
                navigate("/")
            })
            .catch((error) => {
                setCorrect(false)
                console.log(error)
            })
    }


    return (
        <>
            <div className="flex flex-col items-center text-2xl bg-green-400 rounded-b-full" data-testid="login">
                <h1 className=" pe-10 me-10 pt-5 text-4xl font-bold bg-gradient-to-r from-sky-950 to-green-900 text-transparent bg-clip-text">Welcome back,</h1>
                <h1 className="ps-50 pt-5 text-3xl font-bold bg-gradient-to-r from-sky-950 to-green-900 text-transparent bg-clip-text pb-1">please login!</h1>
                <div className="flex flex-col p-20 gap-5">
                    <label>Username:</label><input data-testid="usernameInput" type="text" name="username" value={formData.username} onChange={handleChange} className="bg-green-300 rounded-xl" />
                    <label>Password:</label><input data-testid="passwordInput" type="password" name="password" value={formData.password} onChange={handleChange} className="bg-green-300 rounded-xl" />
                </div>
                {!correct && <h1 className="text-red-800 text-2xl">Wrong username or password!</h1>}
                <div>
                    <button className="mb-5 hover:cursor-pointer bg-green-600 p-2 rounded-2xl hover:bg-green-700 hover:underline" onClick={handleSubmit}>Login</button>
                </div>
            </div>
        </>
    )
}
export default LoginPage;