import { render, screen, cleanup, waitFor } from "@testing-library/react";
import "@testing-library/jest-dom/vitest"
import { describe, it, expect, vi, beforeEach, afterEach } from 'vitest'
import React from "react";
import userEvent from "@testing-library/user-event"
import { MemoryRouter, useNavigate } from "react-router-dom";
import LoginPage from "../LoginPage.jsx";
import AuthContext from "../../../../context/AuthContext.js";
import useAuth from "../../../../hooks/useAuth.js";
import userRepository from "../../../../repository/userRepository.js";

const mockLogin = vi.fn()
const mockUseNavigate = vi.fn()
const mockRepositoryLogin = vi.fn();

const mockAuthContextValue = {
    login: mockLogin,
};



vi.mock("../../../hooks/useAuth.js", () => ({
    default: () => ({
        login: mockLogin
    })
}));


vi.mock("react-router-dom", async (importOriginal) => {
    const actual = await importOriginal()
    return {
        ...actual,
        useNavigate: () => mockUseNavigate
    }
})

vi.mock('../../../../repository/userRepository.js', () => {
    const mockRepositoryLogin = vi.fn(); 
    return {
        default: {
            login: mockRepositoryLogin,
        },
    }
});



describe("Login page test", () => {

    beforeEach(() => {
        cleanup();
        vi.clearAllMocks();
    });
    it("Renders successfully", () => {
        render(
            <AuthContext.Provider value={mockAuthContextValue}>
                <MemoryRouter>
                    <LoginPage />
                </MemoryRouter>
            </AuthContext.Provider>
        )
        expect(screen.getByTestId("login")).toBeInTheDocument()
        expect(screen.getByRole("button")).toBeInTheDocument()
    })

    it("Handles change", async () => {
        render(
            <AuthContext.Provider value={mockAuthContextValue}>
                <MemoryRouter>
                    <LoginPage />
                </MemoryRouter>
            </AuthContext.Provider>
        )

        const usernameInput = screen.getByTestId("usernameInput")
        const passwordInput = screen.getByTestId("passwordInput")

        await userEvent.type(usernameInput, "user")
        await userEvent.type(passwordInput, "pw")


        expect(usernameInput).toHaveValue("user")
        expect(passwordInput).toHaveValue("pw")
    })


it("calls repository.login, context.login and navigates on successful submit", async () => {
    const mockedToken = { data: { accessToken: 'mocked-token' } };
    userRepository.login.mockResolvedValueOnce(mockedToken);


    render(
        <AuthContext.Provider value={{ login: mockLogin }}>
            <MemoryRouter>
                <LoginPage />
            </MemoryRouter>
        </AuthContext.Provider>
    );

    await userEvent.type(screen.getByTestId("usernameInput"), "user");
    await userEvent.type(screen.getByTestId("passwordInput"), "pw");
    await userEvent.click(screen.getByRole("button"));

    await waitFor(() => {
        expect(userRepository.login).toHaveBeenCalledTimes(1);
        expect(mockLogin).toHaveBeenCalledWith("mocked-token");
        expect(mockUseNavigate).toHaveBeenCalledWith("/");
    });
});

})

// const handleSubmit = () => {
//     userRepository
//         .login(formData)
//         .then((response) => {
//             setCorrect(true)
//             login(response.data.accessToken)
//             navigate("/")
//         })
//         .catch((error) => {
//             setCorrect(false)
//             console.log(error)
//         })
// }