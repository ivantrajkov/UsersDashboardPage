import { render, renderHook, screen, waitFor , act, cleanup } from "@testing-library/react";
import "@testing-library/jest-dom/vitest"
import userEvent from "@testing-library/user-event"
import { describe, it, expect, vi, beforeEach, afterEach } from 'vitest'
import userRepository from "../../repository/userRepository";
import useUsers from "../useUsers";

vi.mock("../../repository/userRepository", () => ({
    default: {
        findAll: vi.fn(),
        delete: vi.fn(),
    }
}));

const mockUsers = [{ username: "user1" }, { username: "user2" }];
describe("useUsers tests", () => {
    beforeEach(() => {
        cleanup()
        vi.clearAllMocks();
    });
    it("hook fetches users", async () => {
        userRepository.findAll.mockResolvedValueOnce({data: mockUsers})
        const { result } = renderHook(() => useUsers())

         await waitFor(() => expect(result.current.users).toEqual(mockUsers));

    })
    it("hook deletes a user", async () => {
        const mockUsers = [{ username: "user1" }, { username: "user2" }];
    userRepository.findAll.mockResolvedValueOnce({ data: mockUsers });
    userRepository.delete.mockResolvedValueOnce({});

    const { result } = renderHook(() => useUsers());

    await waitFor(() => expect(result.current.users).toEqual(mockUsers));

    act(() => {
      result.current.onDelete();
    });

    await userRepository.findAll.mockResolvedValueOnce({ data: [{ username: "user2" }] });

    await waitFor(() => expect(result.current.users).toEqual([{ username: "user2" }]));
  });
})