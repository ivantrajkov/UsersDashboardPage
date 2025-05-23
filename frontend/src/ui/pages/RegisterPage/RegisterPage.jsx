import { useNavigate } from "react-router-dom"
import { useState } from "react";
import userRepository from "../../../repository/userRepository.js"
import useAuth from "../../../hooks/useAuth.js"
import React from "react";
const initialFormData = {
    "username": "",
    "password": ""
};


const RegisterPage = () => {
    const navigate = useNavigate();
    const [formData, setFormData] = useState(initialFormData);


    const handleChange = (event) => {
        const { name, value } = event.target;
        setFormData({ ...formData, [name]: value });
    };

    const handleSubmit = () => {
        userRepository
            .register(formData)
            .then(() => {
                // setFormData(initialFormData);
                navigate("/login");
            })
            .catch();
    };

    return (
        <>
            <div className="flex flex-col items-center text-2xl bg-green-400 rounded-b-full" data-testid="register">
                <h1 className=" pe-10 me-10 pt-5 text-4xl font-bold bg-gradient-to-r from-sky-950 to-green-900 text-transparent bg-clip-text pb-1">Welcome guest,</h1>
                <h1 className="ps-50 pt-5 text-3xl font-bold bg-gradient-to-r from-sky-950 to-green-900 text-transparent bg-clip-text pb-1">please register!</h1>
                <div className="flex flex-col p-20 gap-5">
                    <label htmlFor="username-input">Username:</label>
                    <input
                        type="text"
                        id="username-input"
                        name="username"
                        value={formData.username}
                        onChange={handleChange}
                        className="bg-green-300 rounded-xl"
                        data-testid="username-input"
                    />

                    <label htmlFor="password-input">Password:</label>
                    <input
                        type="password"
                        id="password-input"
                        name="password"
                        value={formData.password}
                        onChange={handleChange}
                        className="bg-green-300 rounded-xl"
                        data-testid="password-input"
                    />
                </div>
                <div>
                    <button className="mb-5 hover:cursor-pointer bg-green-600 p-2 rounded-2xl hover:bg-green-700 hover:underline" onClick={handleSubmit}>Login</button>
                </div>
            </div>
        </>
    )


}
export default RegisterPage;