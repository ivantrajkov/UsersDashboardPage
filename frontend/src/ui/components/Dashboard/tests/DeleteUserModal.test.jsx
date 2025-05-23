import "@testing-library/jest-dom/vitest"
import { describe, it, expect, vi, beforeEach, afterEach } from 'vitest'
import { render, screen, cleanup } from "@testing-library/react";
import React from "react";
import UserCard from "../UserCard.jsx";
import userEvent from "@testing-library/user-event"
import DeleteUserModal from "../DeleteUserModal.jsx";

const users = [
    {
        "id": 1,
        "username": "Username1"
    },
    {
        "id": 2,
        "username": "Username2"
    }
]
const user = users[0]
describe("DeleteUserModal Test", () => {
    const onClose = vi.fn();
    const onDelete = vi.fn();

    afterEach(() => {
        cleanup()
    })

    it("Renders successfully", () => {
        render(<DeleteUserModal user={user} />)
        expect(screen.getByTestId("deleteModal")).toBeInTheDocument()
    })

    it("On click it calls onClose",async () => {
        render(<DeleteUserModal user={user} onClose={onClose} onDelete={onDelete} />)
        // const button = screen.getByRole("button", "Cancel")
        const button = screen.getByText(/cancel/i)
        await userEvent.click(button)
        // screen.debug()
        expect(onClose).toHaveBeenCalledTimes(1)
        expect(onDelete).not.toHaveBeenCalled(1)
    })
    
    it("On click it calls onDelete",async () => {
        render(<DeleteUserModal user={user} onClose={onClose} onDelete={onDelete} />)
        // const button = screen.getByRole("button", "Deactivate")
        // const button = screen.getByText(/deactivate/i)
        const button = screen.getByTestId("btn-deactivate")
        await userEvent.click(button)
        // screen.debug()
        expect(onClose).not.toHaveBeenCalledTimes(1)
        expect(onDelete).toHaveBeenCalled(1)
    })
})
