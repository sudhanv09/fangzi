"use client";

import {
  Group,
  Button,
  Box,
  Anchor,
  UnstyledButton,
  Avatar,
  Menu,
  Text
} from "@mantine/core";

import classes from "./NavHeader.module.css";
import React from "react";

export default function Header() {
  return (
    <Box pb={120}>
      <header className={classes.header}>
        <Group justify="space-between" h="100%">
          <Anchor href="/" underline="never">
            <Text size="xl" fw={700} c="black">Fangzi</Text>
          </Anchor>
          <Group visibleFrom="sm">
            <Button component="a" href="/login" variant="default">
              Log in
            </Button>
            <Button component="a" href="/register">
              Sign up
            </Button>
            <Menu>
              <Menu.Target>
                <UnstyledButton>
                  <Group gap={7}>
                    <Avatar src="./next.svg" alt="" radius="xl" size={20} />
                  </Group>
                </UnstyledButton>
              </Menu.Target>
              <Menu.Dropdown>
                <Menu.Item>
                  <Anchor href="/user" target="_blank" underline="never">
                    Profile
                  </Anchor>
                </Menu.Item>
                <Menu.Item>
                  {" "}
                  <Anchor href="/logout" target="_blank" underline="never">
                    Logout
                  </Anchor>
                </Menu.Item>
              </Menu.Dropdown>
            </Menu>
          </Group>
        </Group>
      </header>
    </Box>
  );
}
