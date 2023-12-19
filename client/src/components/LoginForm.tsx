"use client";

import {
  TextInput,
  PasswordInput,
  Checkbox,
  Anchor,
  Paper,
  Title,
  Text,
  Container,
  Group,
  Button,
} from "@mantine/core";
import React, { useState } from "react";
import useLogin from "@/hooks/useLogin";
import classes from "./LoginForm.module.css";

export default async function LoginForm() {
  const [user_email, setuserEmail] = useState("");
  const [user_password, setuserPassword] = useState("");
  
  return (
    <Container size={420} my={40}>
      <Title ta="center" className={classes.title}>
        Welcome back!
      </Title>

      <Paper withBorder shadow="md" p={30} mt={30} radius="md">
        <TextInput
          label="Email"
          placeholder="you@mantine.dev"
          required
          value={user_email}
          onChange={(e) => setuserEmail(e.target.value)}
        />
        <PasswordInput
          label="Password"
          placeholder="Your password"
          required
          mt="md"
          value={user_password}
          onChange={(e) => setuserPassword(e.target.value)}
        />
        <Group justify="space-between" mt="lg">
          <Checkbox label="Remember me" />
          <Anchor component="button" size="sm">
            Forgot password?
          </Anchor>
        </Group>
        <Button fullWidth mt="xl">
          Sign in
        </Button>
      </Paper>

      <Text c="dimmed" size="sm" ta="center" mt={20}>
        Do not have an account yet?{" "}
        <Anchor href="/register">Create account</Anchor>
      </Text>
    </Container>
  );
}
