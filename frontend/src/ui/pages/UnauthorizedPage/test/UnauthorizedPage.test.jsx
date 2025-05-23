import "@testing-library/jest-dom/vitest"
import { describe, it, expect, vi, beforeEach, afterEach } from 'vitest'
import { render, screen, cleanup } from "@testing-library/react";
import React from "react";
import UnauthorizedPage from "../UnauthorizedPage.jsx";
// import UserCard from "../UserCard.jsx";
import userEvent from "@testing-library/user-event"


const mockNavigate = vi.fn()

vi.mock('react-router-dom', async (importOriginal) => {
    const actual = await importOriginal();
    return {
        ...actual,
        useNavigate: () => mockNavigate
    }
})

beforeEach(() => {
    cleanup()
})

describe("Unauthorized Page test", () => {
    it("Renders the page successfully", () => {
        render(<UnauthorizedPage />);
        // const button = screen.getByTestId("unauthorized")
        expect(screen.getByTestId("unauthorized")).toBeInTheDocument()
        expect(screen.getByRole("button")).toBeInTheDocument()
    })
    it("Navigate to /login", async () => {
        render(<UnauthorizedPage />);
        expect(screen.getByRole("button")).toBeInTheDocument()
        const button = screen.getByRole("button")
        await userEvent.click(button)
        expect(mockNavigate).toHaveBeenCalledTimes(1)
        expect(mockNavigate).toHaveBeenCalledWith("/login");
    })
})