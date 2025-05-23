// src/ui/pages/RegisterPage/RegisterPage.test.jsx
import { render, screen, cleanup, waitFor } from "@testing-library/react";
import "@testing-library/jest-dom/vitest"
import { describe, it, expect, vi, beforeEach, afterEach } from 'vitest'
import React from "react";
import userEvent from "@testing-library/user-event"
import { MemoryRouter } from "react-router-dom";
import LoggedOutPage from "../LoggedOutPage";

describe("Logged out page testing", () => {
    it("Renders successfully", () => {
        render(<MemoryRouter><LoggedOutPage/></MemoryRouter>)
        expect(screen.getByTestId("loggedOut")).toBeInTheDocument()
    })
})