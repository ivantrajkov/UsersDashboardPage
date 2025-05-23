import { Home } from "../Home.jsx";
import "@testing-library/jest-dom/vitest"
import { describe, it, expect, vi, beforeEach, afterEach } from 'vitest'
import { render, screen, waitFor } from "@testing-library/react";
import React from "react";

// vi.mock('../../components/Home/HomeBanner/HomeBanner', () => ({
//     default: () => <div>Mocked Home Banner</div>,
// }));
// vi.mock('../../components/Home/Review/ReviewGrid/ReviewGrid', () => ({
//     default: () => <div>Mocked Review Grid</div>, 

// }));

// vi.mock('../../components/Home/Review/ReviewCard', () => ({
//     default: () => <div>Mocked Review Card</div>, // Add this mock
// }));

describe("Render home page", () => {
    it("renders the homepage sucessfully", () => {
        render(<Home/>)
        expect(screen.getByTestId("image")).toBeInTheDocument()
        expect(screen.getByTestId("grid")).toBeInTheDocument()
        expect(screen.getAllByTestId("card")).toHaveLength(4)
        // expect(screen.getByTestId("card")).toBeInTheDocument()
    })
})