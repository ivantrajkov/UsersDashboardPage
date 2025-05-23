import "@testing-library/jest-dom/vitest"
import { describe, it, expect, vi, beforeEach, afterEach } from 'vitest'
import { render, screen, waitFor } from "@testing-library/react";
import React from "react";
import UserGrid from "../UserGrid";

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

describe("Renders correctly", () =>{
    it("Maps the users correctly", () => {
        render(<UserGrid users={users} />)
        expect(screen.getByTestId("grid")).toBeInTheDocument()
        expect(screen.queryAllByRole('paragraph')).toHaveLength(users.length)
    })
})