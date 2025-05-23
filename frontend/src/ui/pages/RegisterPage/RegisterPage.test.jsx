// src/ui/pages/RegisterPage/RegisterPage.test.jsx
import { render, screen, cleanup, waitFor } from "@testing-library/react";
import "@testing-library/jest-dom/vitest"
import { describe, it, expect, vi, beforeEach, afterEach } from 'vitest'
import React from "react";
import userEvent from "@testing-library/user-event"
import RegisterPage from "./RegisterPage";
import { MemoryRouter } from "react-router-dom";
import userRepository from '../../../repository/userRepository.js';
// Keep your mocks for useNavigate and userRepository at the top
const mockNavigate = vi.fn();
vi.mock('react-router-dom', async (importOriginal) => {
    const actual = await importOriginal();
    return {
        ...actual,
        useNavigate: () => mockNavigate,
    };
});

vi.mock('../../../repository/userRepository.js', () => ({
    default: {
        register: vi.fn(), // This mock needs to be present
    },
}));


describe("Register page test", () => {
    let userInteractions;

    beforeEach(() => {
        cleanup();
        vi.clearAllMocks(); // Essential for resetting mock calls before each test
        userInteractions = userEvent.setup();
    });

    it("Renders successfully", () => {
        render(
            <MemoryRouter>
                <RegisterPage />
            </MemoryRouter>
        );
        expect(screen.getByTestId("register")).toBeInTheDocument();
        expect(screen.getByRole("button", { name: /login/i })).toBeInTheDocument();
        expect(screen.getByLabelText(/username:/i)).toBeInTheDocument();
        expect(screen.getByLabelText(/password:/i)).toBeInTheDocument();
    });

    it("Handles change in writing", async () => {
        render(
            <MemoryRouter>
                <RegisterPage />
            </MemoryRouter>
        );

        const usernameInput = screen.getByLabelText(/username:/i);
        const passwordInput = screen.getByLabelText(/password:/i);

        expect(usernameInput).toBeInTheDocument();
        expect(passwordInput).toBeInTheDocument();

        await userInteractions.type(usernameInput, "username1");
        await userInteractions.type(passwordInput, "password1");

        expect(usernameInput).toHaveValue("username1");
        expect(passwordInput).toHaveValue("password1");
    });

    // You still need to implement this test fully, this is where userRepository.register assertions go.
    it("On register it submits the login", async () => {
        // const button = screen.getByRole("button")
        render(
            <MemoryRouter>
                <RegisterPage />
            </MemoryRouter>
        );
        userRepository.register.mockResolvedValueOnce({});
        const button = screen.getByRole("button")
        const usernameInput = screen.getByLabelText(/username:/i);
        const passwordInput = screen.getByLabelText(/password:/i);


        await userInteractions.type(usernameInput, "username1");
        await userInteractions.type(passwordInput, "password1");
        await userInteractions.click(button)
        expect(userRepository.register).toHaveBeenCalledTimes(1);
        expect(userRepository.register).toHaveBeenCalledWith({ username: 'username1', password: 'password1' });
        await waitFor(() => {
            expect(mockNavigate).toHaveBeenCalledTimes(1);
            expect(mockNavigate).toHaveBeenCalledWith('/login');
        });

    });
});