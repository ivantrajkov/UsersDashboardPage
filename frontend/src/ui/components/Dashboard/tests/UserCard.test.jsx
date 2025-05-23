import "@testing-library/jest-dom/vitest"
import { describe, it, expect, vi, beforeEach, afterEach } from 'vitest'
import { render, screen, cleanup } from "@testing-library/react";
import React from "react";
import UserCard from "../UserCard.jsx";
import userEvent from "@testing-library/user-event"


const users = [
    {
        "id": 1,
        "username": "Username1"
    },
    {
        "id": 1,
        "username": "Username2"
    }
]
describe("UserCard component test", () => {
const onDelete = vi.fn();
     afterEach(() => {
      cleanup();
    });
    
    it("Renders successfully", () => {
        render(<UserCard user={users[0]} />)
        expect(screen.getByText(/Username1/i)).toBeInTheDocument()
    })

    it("Button opens the modal", async () => {
        render(<UserCard user={users[0]} />)
        const button = screen.getByRole('button')
        expect(screen.getByRole('button')).toBeInTheDocument()
        await userEvent.click(button)
        // screen.debug();
        expect(screen.getByTestId('deleteModal')).toBeInTheDocument()
    })
    it("On Delete User modal on 'Cancel' it returns to the user card screen",async ()=> {
        render(<UserCard user={users[0]} onDelete={onDelete} />)
        const button = screen.getByRole('button')
        expect(screen.getByRole('button')).toBeInTheDocument()
        await userEvent.click(button)
        const cancelBtn = screen.getByTestId("btn-cancel")
        await userEvent.click(cancelBtn)
        expect(screen.getByText(/Username1/i)).toBeInTheDocument()
        // screen.debug();
    })

})